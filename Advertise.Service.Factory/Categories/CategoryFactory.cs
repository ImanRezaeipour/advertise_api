using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Exceptions;
using Advertise.Core.Helpers;
using Advertise.Core.Model.Categories;
using Advertise.Core.Model.Companies;
using Advertise.Core.Model.ExportImport;
using Advertise.Core.Model.General;
using Advertise.Core.Model.Products;
using Advertise.Core.Types;
using Advertise.Service.Categories;
using Advertise.Service.Common;
using Advertise.Service.Companies;
using Advertise.Service.Products;
using Advertise.Service.Seos;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Advertise.Service.Factory.Categories
{
    public class CategoryFactory : ICategoryFactory
    {
        #region Private Fields

        private readonly ICategoryFollowService _categoryFollowService;
        private readonly ICategoryService _categoryService;
        private readonly ICommonService _commonService;
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private readonly ISeoService _seoService;
        private readonly IProductService _productService;
        private readonly IListService _listService;

        #endregion Private Fields

        #region Public Constructors

        public CategoryFactory(ICompanyService companyService, IMapper mapper, ICategoryFollowService categoryFollowService, ICategoryService categoryService, IProductService productService, ICommonService commonService, ISeoService seoService, IListService listService)
        {
            _companyService = companyService;
            _mapper = mapper;
            _categoryFollowService = categoryFollowService;
            _categoryService = categoryService;
            _productService = productService;
            _commonService = commonService;
            _seoService = seoService;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<CategoryBreadCrumbModel> PrepareBreadCrumbModelAsync(Guid categoryId, string currentTitle, bool? isAllSearch)
        {
            var allParents = await _categoryService.GetParentsByIdAsync(categoryId);
            var nodes = _mapper.Map<List<CategoryModel>>(allParents);
            var viewModel = new CategoryBreadCrumbModel
            {
                Nodes = nodes,
                CurrentTitle = currentTitle,
                IsAllSearch = isAllSearch
            };

            return viewModel;
        }

        public async Task<CompanyListModel> PrepareCompanyListModelAsync(Guid categoryId)
        {
            var request = new CompanySearchModel
            {
                CategoryId = categoryId,
                PageIndex = 1
            };
            var categoryCompanies = await _companyService.GetByRequestAsync(request);

            var categoryCompanyViewModel = _mapper.Map<List<CompanyModel>>(categoryCompanies);

            var listViewModel = new CompanyListModel
            {
                SearchModel = request,
                Companies = categoryCompanyViewModel
            };

            return listViewModel;
        }

        public async Task<CategoryCreateModel> PrepareCreateModelAsync()
        {
            var viewModel = new CategoryCreateModel();
            return viewModel;
        }

        public async Task<CategoryDetailModel> PrepareDetailModelAsync(string categoryAlias, string slug)
        {
            var seoExist = await _seoService.IsExistCategoryByIdAsync(categoryAlias);
            if (seoExist)
                throw new ValidationException("Category is modified.");

            var slugResult = await _categoryService.IsCompareNameAndSlugAsync(categoryAlias, slug);
            if (!slugResult)
                throw new ValidationException("عدم تطابق اسلاگ");
            var category = await _categoryService.FindByAliasAsync(categoryAlias);
            if (category == null)
                throw new FactoryException();
            var viewModel = _mapper.Map<CategoryDetailModel>(category);

            var categoryOption = await _categoryService.GetCategoryOptionByIdAsync(category.Id);
            viewModel.CategoryOption = categoryOption != null ? _mapper.Map<CategoryOptionModel>(categoryOption) : null;

            var categories = await _categoryService.GetCategoriesByParentId(viewModel.Id);
            viewModel.Categories = categories != null ? _mapper.Map<List<CategoryModel>>(categories) : null;

            viewModel.CompanyCount = await _companyService.CountByCategoryIdAsync(viewModel.Id);
            viewModel.FollowerCount = await _categoryFollowService.CountAllFollowByCategoryIdAsync(viewModel.Id);
            viewModel.ProductCount = await _productService.CountByCategoryIdAsync(viewModel.Id);
            viewModel.InitFollow = await _categoryFollowService.IsFollowCurrentUserByCategoryIdAsync(viewModel.Id);

            return viewModel;
        }

        public async Task<CategoryEditModel> PrepareEditModelAsync(string categoryAlias)
        {
            var category = await _categoryService.FindByAliasAsync(categoryAlias);
            if (category == null)
                throw new FactoryException();
            var viewModel = _mapper.Map<CategoryEditModel>(category);

            var categoryOption = await _categoryService.GetCategoryOptionByIdAsync(category.Id);
            viewModel.CategoryOptionList = new[]
            {
                new SelectList
                {
                    Text = categoryOption.Title,
                    Value = categoryOption.Id.ToString()
                }
            };

            return viewModel;
        }

        public async Task<CategoryListModel> PrepareListModelAsync(CategorySearchModel request, bool isCurrentUser = false, Guid? userId = null)
        {
            request.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            var categories = await _categoryService.GetByRequestAsync(request);
            var categoriesMap = _mapper.Map<List<CategoryModel>>(categories);
            request.TotalCount = await _categoryService.CountByRequestAsync(request);

            var categoriesList = new CategoryListModel
            {
                SearchRequest = request,
                Categories = categoriesMap,
                SortDirectionList = await _listService.GetSortDirectionListAsync(),
                ActiveList = EnumHelper.CastToSelectListItems<ActiveType>(),
                PageSizeList = await _listService.GetPageSizeListAsync()
            };

            return categoriesList;
        }

        public async Task<MainMenuModel> PrepareMainMenuModelAsync()
        {
            var viewModel = new MainMenuModel();

            var request = new CategorySearchModel
            {
                PageSize = PageSize.All
            };
            var categories = _categoryService.QueryByRequest(request);
            viewModel.Categories = await categories.ProjectTo<CategoryModel>().ToListAsync();

            return viewModel;
        }

        public async Task<ProductListModel> PrepareProductListModelAsync(Guid categoryId)
        {
            var request = new ProductSearchModel
            {
                CategoryId = categoryId
            };
            var categoryProducts = await _productService.GetByRequestAsync(request);
            var categoryProductViewModel = _mapper.Map<List<ProductModel>>(categoryProducts);

            var listViewModel = new ProductListModel
            {
                SearchModel = request,
                Products = categoryProductViewModel
            };

            return listViewModel;
        }
        
        public async Task<ImportIndexModel> PrepareImportIndexModelAsync()
        {
            var viewModel = new ImportIndexModel
            {
                CategoryList = await _categoryService.GetAllAsSelectListAsync()
            };

            return viewModel;
        }
        
        public async Task<ExportIndexModel> PrepareExportIndexModelAsync()
        {
            var viewModel = new ExportIndexModel
            {
                CategoryList = await _categoryService.GetAllAsSelectListAsync()
            };

            return viewModel;
        }

        #endregion Public Methods
    }
}