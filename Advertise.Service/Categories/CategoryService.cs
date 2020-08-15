using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using System.Web;
using Advertise.Core.Common;
using Advertise.Core.Domain.Categories;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Managers.ExportImport;
using Advertise.Core.Managers.ExportImport.Help;
using Advertise.Core.Managers.File;
using Advertise.Core.Managers.Node;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Catalogs;
using Advertise.Core.Model.Categories;
using Advertise.Core.Model.Common;
using Advertise.Core.Objects;
using Advertise.Core.Types;
using Advertise.Data.DbContexts;
using Advertise.Service.Companies;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using OfficeOpenXml;

namespace Advertise.Service.Categories
{
    public class CategoryService : ICategoryService
    {
        #region Private Fields

        private readonly IDbSet<Category> _categoryRepository;
        private readonly IDbSet<Company> _companyRepository;
        private readonly ICompanyService _companyService;
        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public CategoryService(IMapper mapper, IUnitOfWork unitOfWork, ICompanyService companyService, IWebContextManager webContextManager, IEventPublisher eventPublisher)
        {
            _mapper = mapper;
            _categoryRepository = unitOfWork.Set<Category>();
            _unitOfWork = unitOfWork;
            _companyService = companyService;
            _webContextManager = webContextManager;
            _eventPublisher = eventPublisher;
            _companyRepository = unitOfWork.Set<Company>();
        }

        #endregion Public Constructors
        
        public Guid CurrentCategoryId => _companyRepository.AsNoTracking().Where(m => m.CreatedById == _webContextManager.CurrentUserId).Select(m => m.CategoryId).SingleOrDefault() ?? Guid.Empty;

        #region Public Methods

        public async Task<int> CountAllAsync()
        {
            var request = new CategorySearchModel
            {
                PageSize = PageSize.All,
            };
            var result = await CountByRequestAsync(request);

            return result;
        }

        public async Task<int> CountByRequestAsync(CategorySearchModel request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var categories = await QueryByRequest(request).CountAsync();

            return categories;
        }

        public async Task CreateByViewModelAsync(CategoryCreateModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentException(nameof(viewModel));

            var category = _mapper.Map<Category>(viewModel);
            category.IsActive = true;
            category.MetaTitle = category.Title;
            category.MetaDescription = category.Description;
            category.Alias = viewModel.Alias;
            category.Order = viewModel.Order;
            category.CreatedById = _webContextManager.CurrentUserId;
            _categoryRepository.Add(category);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityInserted(category);
        }

        public async Task DeleteByAliasAsync(string categoryAlias)
        {
            var category = await _categoryRepository.FirstOrDefaultAsync(model => model.Alias == categoryAlias);
            _categoryRepository.Remove(category);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(category);
        }

        public async Task DeleteByIdAsync(Guid categoryId)
        {
            var category = await FindByIdAsync(categoryId);
            _categoryRepository.Remove(category);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(category);
        }

        public async Task EditByViewModelAsync(CategoryEditModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            var orginalCategory = await _categoryRepository.FirstOrDefaultAsync(model => model.Id == viewModel.Id);
            _mapper.Map(viewModel, orginalCategory);
            orginalCategory.ParentId = viewModel.ParentId;

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(orginalCategory);
        }

        public async Task<Category> FindByAliasAsync(string categoryAlias)
        {
            return await _categoryRepository.FirstOrDefaultAsync(model => model.Alias == categoryAlias);
        }

        public async Task<Category> FindByCodeAsync(string categoryCode)
        {
            return await _categoryRepository.FirstOrDefaultAsync(model => model.Code == categoryCode);
        }

        public async Task<Category> FindByIdAsync(Guid categoryId)
        {
            return await _categoryRepository.FirstOrDefaultAsync(model => model.Id == categoryId);
        }

        public async Task<Category> FindParentAsync(Guid categoryId)
        {
            var child = await _categoryRepository.FirstOrDefaultAsync(model => model.Id == categoryId);
            var parent = await _categoryRepository.FirstOrDefaultAsync(model => model.Id == child.ParentId);

            return parent;
        }

        public async Task<List<SelectList>> GetAllAsSelectListAsync()
        {
            return await _categoryRepository
                .Where(model => model.IsActive == true)
                .Select(model => new SelectList
                {
                    Value = model.Id.ToString(),
                    Text = model.Title
                }).ToListAsync();
        }

        public async Task<object> GetAllAsync()
        {
            var request = new CategorySearchModel
            {
                SortMember = SortMember.HasChild,
                SortDirection = SortDirection.Desc
            };
            var categories = QueryByRequest(request);
            var categoriesViewModel = await categories.ProjectTo<CategoryModel>().ToListAsync();
            return categoriesViewModel.Select(model => new
            {
                model.Id,
                model.HasChild,
                model.Title,
                model.Alias,
                model.Icon,
                model.ParentId,
                Type = model.Type.ToString()
            });
        }

        public async Task<IList<CategoryModel>> GetAllSalableTypeAsync()
        {
            var request = new CategorySearchModel
            {
                SortMember = SortMember.HasChild,
                SortDirection = SortDirection.Desc,
                Type = CategoryType.Salable,
                WithRoot = true
            };
            var categories = QueryByRequest(request);
            return await categories.ProjectTo<CategoryModel>().ToListAsync();
        }

        public async Task<IList<Category>> GetByRequestAsync(CategorySearchModel request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return await QueryByRequest(request).ToPagedListAsync(request.PageIndex, request.PageSize);
        }

        public async Task<IList<CategoryModel>> GetAsViewModelByRequestAsync(CategorySearchModel request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return await QueryByRequest(request).ProjectTo<CategoryModel>().ToPagedListAsync();
        }

        public async Task<IList<CategoryModel>> GetCategoriesByParentId(Guid parentId)
        {
            var request = new CategorySearchModel
            {
                PageSize = PageSize.All,
                SortMember = SortMember.Title,
                ParentId = parentId,
                IsActive = true
            };
            var categories = QueryByRequest(request);
            var viewModel = await categories.ProjectTo<CategoryModel>().ToListAsync();

            return viewModel;
        }

        public async Task<CategoryOption> GetCategoryOptionByIdAsync(Guid id)
        {
            return await _categoryRepository
                  .AsNoTracking()
                  .Include(model => model.CategoryOption)
                  .Where(model => model.Id == id)
                  .Select(model => model.CategoryOption)
                  .FirstOrDefaultAsync();
        }

        public async Task<IList<Category>> GetChildsByIdAsync(Guid categoryId)
        {
            var childs = await _categoryRepository
                .AsNoTracking()
                .Where(model => model.ParentId == categoryId)
                .ToListAsync();

            return childs;
        }

        public async Task<IList<FineUploaderObject>> GetFileAsFineUploaderModelByIdAsync(Guid categoryId)
        {
            return (await _categoryRepository.AsNoTracking()
                    .Where(model => model.Id == categoryId)
                    .Select(model => new
                    {
                        model.Id,
                        model.ImageFileName
                    }).ToListAsync())
                .Select(model => new FineUploaderObject
                {
                    id = model.Id.ToString(),
                    uuid = model.Id.ToString(),
                    name = model.ImageFileName,
                    thumbnailUrl = FileConst.ImagesWebPath.PlusWord(model.ImageFileName),
                    size = new FileInfo(HttpContext.Current.Server.MapPath(FileConst.ImagesWebPath.PlusWord(model.ImageFileName))).Length.ToString()
                }).ToList();
        }

        public async Task<Guid> GetIdByAliasAsync(string alias)
        {
            if(string.IsNullOrEmpty(alias))
                throw new Exception("alias is empty.");
            
            return await _categoryRepository.AsNoTracking().Where(model => model.Alias == alias)
                .Select(model => model.Id).FirstOrDefaultAsync();
        }

        public async Task<IList<SelectList>> GetMainCategoriesAsSelectListItemAsync()
        {
            var request = new CategorySearchModel
            {
                PageSize = PageSize.Count100,
                SortMember = SortMember.Title,
                ParentId = (await GetRootAsync()).Id,
                IsActive = true
            };
            var categories = await GetByRequestAsync(request);
            
            return categories.Select(model => new SelectList
            {
                Value = model.Id.ToString(),
                Text = model.Title
            }).ToList();
        }

        public async Task<IList<CategoryModel>> GetMainCategoriesAsync()
        {
            var request = new CategorySearchModel
            {
                PageSize = PageSize.Count100,
                SortMember = SortMember.Title,
                ParentId = (await GetRootAsync()).Id,
                IsActive = true
            };
            var categories = QueryByRequest(request);
            return await categories.ProjectTo<CategoryModel>().ToListAsync();
        }

        public async Task<IList<Category>> GetParentsByIdAsync(Guid categoryId, bool? withRoot = false)
        {
            var category = await _categoryRepository.AsNoTracking().FirstOrDefaultAsync(model => model.Id == categoryId);

            if (category == null)
                return null;

            if (category.ParentId == null && category.Level == 0 && withRoot == true)
                return new[] {category};

            if (category.ParentId == null)
                return new List<Category>();

            return new[] {category}.Concat(await GetParentsByIdAsync(category.ParentId.Value, withRoot)).Reverse().ToList();
        }

        public async Task<IList<CategoryModel>> GetRaletedCategoriesByAliasAsync(string categoryAlias)
        {
            var categoryId = await GetIdByAliasAsync(categoryAlias);
            var categoryChilds = await GetChildsByIdAsync(categoryId);
            var categoryParents = await GetParentsByIdAsync(categoryId, true);
            //if (categoryParents.Capacity > 1)
            //    categoryParents[1].ParentId = null;
            //else
            //    categoryParents[1].ParentId = null;
            var categoryList = _mapper.Map<IList<CategoryModel>>(categoryChilds.Union(categoryParents));
            return categoryList;
        }

        public async Task<Category> GetRootAsync()
        {
            return await _categoryRepository
                .AsNoTracking()
                .FirstOrDefaultAsync(model => model.ParentId == null);
        }

        public async Task<IList<SelectList>> GetAllowedAsSelectListAsync()
        {
            var subCategories = await GetAllChildsByIdAsync(CurrentCategoryId);

            return await _categoryRepository.AsNoTracking()
                .Where(model => subCategories.Contains(model.Id))
                .Select(model => new SelectList
                {
                    Text = model.Title,
                    Value = model.Id.ToString()
                })
                .ToListAsync();
        }

        public async Task<IList<Select2Object>> GetAllowedAsSelect2ObjectAsync()
        {
            var rootCategory = await FindByIdAsync(CurrentCategoryId);
            var allCategories = await _categoryRepository.AsNoTracking().OrderBy(model => model.Order).ToListAsync();
            var subCategories = await GetAllChildsByIdAsync(allCategories, rootCategory);

            return subCategories.Select(model => new Select2Object
            {
                id = model.Id,
                text = model.Title,
                level = model.Level.Value,
                disabled = model.HasChild.Value
            })
                .ToList();
        }

        public async Task<IList<Guid>> GetAllChildsByIdAsync(Guid categoryId)
        {
            var allCategories = (await GetByRequestAsync(new CategorySearchModel
            {
                PageSize = PageSize.All
            })).ToList();
            var category = await FindByIdAsync(CurrentCategoryId);
            return (await GetAllChildsByIdAsync(allCategories, category))
                .Select(model => model.Id)
                .ToList();
        }

        public async Task<IEnumerable<Category>> GetAllChildsByIdAsync(List<Category> categoryList, Category category)
        {
            var childs = categoryList.Where(model => model.ParentId == category.Id).ToList();

            if (childs.Count <= 0)
                return Enumerable.Empty<Category>();

            var childList = new List<Category>();
            foreach (var child in childs)
            {
                childList.AddRange(new[] { child }.Concat(await GetAllChildsByIdAsync(categoryList, child)));
            }

            return childList;
        }

        public async Task<object> GetSubCategoriesByParentIdAsync(Guid parentId)
        {
            return await _categoryRepository
                .AsNoTracking()
                .Where(model => model.ParentId == parentId)
                .Select(model => new { model.Id, model.Title })
                .OrderBy(model => model.Title)
                .ToListAsync();
        }

        public async Task<object> GetSubCatergoriesWithRootByIdAsync(Guid categoryId)
        {
            var categories = await _categoryRepository.AsNoTracking().ToListAsync();
            var category = categories.FirstOrDefault(model => model.Id == categoryId);

            var parentIds = categories.GetAllParentsByIdAsync(category).Select(model => model.Id);
            var childIds = categories.GetAllChildsById(category).Select(model => model.Id);

            var categoryList = _categoryRepository
                .AsNoTracking()
                .AsQueryable()
                .Where(model => model.Id == categoryId || parentIds.Contains(model.Id) || childIds.Contains(model.Id));

            var categoryListViewModel = await categoryList.ProjectTo<CategoryModel>().ToListAsync();
            var result = categoryListViewModel.Select(model => new
            {
                model.Id,
                model.HasChild,
                model.Title,
                model.Alias,
                model.Icon,
                model.ParentId,
                Type = model.Type.ToString()
            });

            return result;
        }

        public async Task<bool> IsCompareNameAndSlugAsync(string alias, string slug)
        {
            if (string.IsNullOrEmpty(alias))
                return false;

            if (string.IsNullOrEmpty(slug))
                return true;

            var category = await _categoryRepository.AsNoTracking().SingleOrDefaultAsync(model => model.Alias == alias);
            return category.MetaTitle.CastToSlug() == slug;
        }

        public async Task<bool> IsRootAsync(Guid categoryId)
        {
            return await _categoryRepository.AnyAsync(model => model.Id == categoryId && model.ParentId == null);
        }

        public IQueryable<Category> QueryByRequest(CategorySearchModel request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var categories = _categoryRepository.AsNoTracking().AsQueryable()
                .Include(model => model.CategoryOption);

            if (request.CreatedById.HasValue)
                categories = categories.Where(model => model.CreatedById == request.CreatedById);
            if (request.Term.HasValue())
                categories = categories.Where(model => model.Title.Contains(request.Term) || model.Description.Contains(request.Term));
            if (request.IsActive.HasValue)
                if (request.IsActive.Value || request.IsActive.Value == false)
                    categories = categories.Where(model => model.IsActive == request.IsActive);
            if (request.ParentId.HasValue && request.WithMany == null)
                categories = categories.Where(model => model.ParentId == request.ParentId);
            if (request.ParentId.HasValue && request.WithMany == true)
                categories = categories.Where(model => model.ParentId == request.ParentId).SelectMany(model => model.Childrens);
            if (request.Id.HasValue && request.WithMany == null)
                categories = categories.Where(model => model.Id == request.Id);
            if (request.Id.HasValue && request.WithMany == true)
                categories = categories.Where(model => model.Id == request.Id).SelectMany(model => model.Childrens);
            if (request.Type.HasValue)
                categories = categories.Where(model => model.Type == request.Type);
            if (request.WithRoot == true)
                categories = categories.Where(model => model.ParentId == null);

            if (string.IsNullOrEmpty(request.SortMember))
                request.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(request.SortDirection))
                request.SortDirection = SortDirection.Desc;

            categories = categories.OrderBy($"{request.SortMember} {request.SortDirection}");

            return categories;
        }

        public async Task SeedAsync()
        {
            var category = new Category()
            {
                ParentId = null,
                CreatedById = _webContextManager.CurrentUserId
            };
            _categoryRepository.Add(category);

            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task FixAllNodesLevel()
        {
            //_unitOfWork.AutoDetectChangesEnabled = false;                     

            var categories = _categoryRepository.ToList();

            var categoriesViewModel = categories
                .Select(category => new CategoryModel
                {
                    Id = category.Id,
                    ParentId = category.ParentId
                })
                .ToList();
            var rootNode = NodeManager<CategoryModel>.CreateTree(categoriesViewModel, node => node.Id, node => node.ParentId).Single();

            categories.ForEach(model =>
                {
                    model.Level = rootNode.All.Where(node => node.Value.Id == model.Id).Select(node => node.Level).FirstOrDefault();
                    model.HasChild = model.Childrens.Any();
                }
            );

            //_categorieRepository.Update(category => new Category
            //{
            //    Level = rootNode.All.Where(node => node.Value.Id == Guid.NewGuid()).Select(node => node.Level).FirstOrDefault(),
            //    HasChild = category.Childrens.Any()
            //});

            _unitOfWork.SaveAllChanges();
        }
        
        public async Task ImportCategoriesFromXlsxAsync(Stream stream)
        {
            using (var xlPackage = new ExcelPackage(stream))
            {
                // get the first worksheet in the workbook
                var worksheet = xlPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    throw new Exception("No worksheet found");

                //the columns
                var properties = new ImportManager().GetPropertiesByExcelCells<CategoryModel>(worksheet);

                var manager = new PropertyManager<CategoryModel>(properties);

                var iRow = 2;
                while (true)
                {
                    var allColumnsAreEmpty = manager.GetProperties
                        .Select(property => worksheet.Cells[iRow, property.PropertyOrderPosition])
                        .All(cell => string.IsNullOrEmpty(cell?.Value?.ToString()));

                    if (allColumnsAreEmpty)
                        break;

                    // read current row data
                    manager.ReadFromXlsx(worksheet, iRow);

                    bool isNew;
                    var categoryViewModel = new CategoryModel();
                    if (string.IsNullOrEmpty(manager.GetProperty("Id").StringValue))
                    {
                        isNew = true;
                    }
                    else
                    {
                        var category = await FindByIdAsync(Guid.Parse(manager.GetProperty("Id").StringValue));
                        if (category == null)
                        {
                            isNew = true;
                        }
                        else
                        {
                            isNew = false;
                            categoryViewModel = _mapper.Map<CategoryModel>(category);
                        }
                    }

                    foreach (var property in manager.GetProperties)
                    {
                        switch (property.PropertyName)
                        {
                            case "Title":
                                categoryViewModel.Title = property.StringValue;
                                break;

                            case "Order":
                                categoryViewModel.Order = property.IntValue;
                                break;

                            case "Alias":
                                categoryViewModel.Alias = property.StringValue;
                                break;
                        }
                    }

                    if (isNew)
                    {
                        var createViewModel = _mapper.Map<CategoryCreateModel>(categoryViewModel);
                        await CreateByViewModelAsync(createViewModel);
                    }
                    else
                    {
                        var editViewModel = _mapper.Map<CategoryEditModel>(categoryViewModel);
                        await EditByViewModelAsync(editViewModel);
                    }

                    iRow++;
                }
            }
        }

        public async Task<byte[]> GetCategoryAsExcelAsync()
        {
            var request = new CategorySearchModel();
            var categories = await GetByRequestAsync(request);
            var vm = _mapper.Map<IList<CategoryModel>>(categories);

            return await ExportCategoriesToXlsxAsync(vm);
        }
        
        public async Task<byte[]> ExportCategoriesToXlsxAsync(IEnumerable<CategoryModel> categories)
        {
            var properties = new[]
            {
                new PropertyByName<CategoryModel>("Id", p => p.Id),
                new PropertyByName<CategoryModel>("Description", p => p.Description),
                new PropertyByName<CategoryModel>("Alias", p => p.Alias),
                new PropertyByName<CategoryModel>("IsActive", p => p.IsActive),
                new PropertyByName<CategoryModel>("Title", p => p.Title),
                new PropertyByName<CategoryModel>("Order", p => p.Order),
                new PropertyByName<CategoryModel>("MetaTitle", p => p.MetaTitle)
            };

            return new ExportManager().ExportToXlsx(properties, categories);
        }

        #endregion Public Methods
    }
}