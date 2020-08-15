using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Exceptions;
using Advertise.Core.Extensions;
using Advertise.Core.Helpers;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Catalogs;
using Advertise.Core.Model.Categories;
using Advertise.Core.Model.Products;
using Advertise.Core.Model.Tags;
using Advertise.Core.Types;
using Advertise.Service.Carts;
using Advertise.Service.Catalogs;
using Advertise.Service.Categories;
using Advertise.Service.Common;
using Advertise.Service.Companies;
using Advertise.Service.Guarantees;
using Advertise.Service.Keywords;
using Advertise.Service.Locations;
using Advertise.Service.Products;
using Advertise.Service.Specifications;
using Advertise.Service.Tags;
using AutoMapper;
using Newtonsoft.Json;

namespace Advertise.Service.Factory.Products
{
    public class ProductFactory : IProductFactory
    {
        #region Private Fields

        private readonly ILocationService _addressService;
        private readonly ICartService _bagService;
        private readonly ICatalogImageService _catalogImageService;
        private readonly ICatalogService _catalogService;
        private readonly ICategoryService _categoryService;
        private readonly ICommonService _commonService;
        private readonly ICompanyService _companyService;
        private readonly IGuaranteeService _guaranteeService;
        private readonly IKeywordService _keywordService;
        private readonly IListService _listManager;
        private readonly IMapper _mapper;
        private readonly IProductCommentService _productCommentService;
        private readonly IProductFeatureService _productFeatureService;
        private readonly IProductImageService _productImageService;
        private readonly IProductKeywordService _productKeywordService;
        private readonly IProductLikeService _productLikeService;
        private readonly IProductRateService _productRateService;
        private readonly IProductReviewService _productReviewService;
        private readonly IProductService _productService;
        private readonly IProductSpecificationService _productSpecificationService;
        private readonly IProductTagService _productTagService;
        private readonly IProductVisitService _productVisitService;
        private readonly ITagService _tagService;
        private readonly IWebContextManager _webContextManager;
        private readonly ICatalogSpecificationService _catalogSpecificationService;
        private readonly ICatalogFeatureService _catalogFeatureService;
        private readonly IProductNotifyService _productNotifyService;
        private readonly ISpecificationService _specificationService;

        #endregion Private Fields

        #region Public Constructors

        public ProductFactory(IListService listManager, ICommonService commonService, ILocationService addressService, ITagService tagService, IProductService productService, IMapper mapper, IProductLikeService productLikeService, IWebContextManager webContextManager, IProductVisitService productVisitService, IProductReviewService productReviewService, IProductImageService productImageService, IProductTagService productTagService, ICartService bagService, IProductFeatureService productFeatureService, IProductCommentService productCommentService, IProductSpecificationService productSpecificationService, ICategoryService categoryService, ICompanyService companyService, IProductRateService productRateService, IKeywordService keywordService, IProductKeywordService productKeywordService, ICatalogService catalogService, IGuaranteeService guaranteeService, ICatalogImageService catalogImageService, ICatalogSpecificationService catalogSpecificationService, ICatalogFeatureService catalogFeatureService, IProductNotifyService productNotifyService, ISpecificationService specificationService)
        {
            _listManager = listManager;
            _commonService = commonService;
            _addressService = addressService;
            _tagService = tagService;
            _productService = productService;
            _mapper = mapper;
            _productLikeService = productLikeService;
            _webContextManager = webContextManager;
            _productVisitService = productVisitService;
            _productReviewService = productReviewService;
            _productImageService = productImageService;
            _productTagService = productTagService;
            _bagService = bagService;
            _productFeatureService = productFeatureService;
            _productCommentService = productCommentService;
            _productSpecificationService = productSpecificationService;
            _categoryService = categoryService;
            _companyService = companyService;
            _productRateService = productRateService;
            _keywordService = keywordService;
            _productKeywordService = productKeywordService;
            _catalogService = catalogService;
            _guaranteeService = guaranteeService;
            _catalogImageService = catalogImageService;
            _catalogSpecificationService = catalogSpecificationService;
            _catalogFeatureService = catalogFeatureService;
            _productNotifyService = productNotifyService;
            _specificationService = specificationService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<ProductBulkCreateModel> PrepareBulkCreateModelAsync(ProductBulkCreateModel modelPrepare = null)
        {
            var viewModel = modelPrepare ?? new ProductBulkCreateModel();

            viewModel.CategoryListJson = JsonConvert.SerializeObject(await _categoryService.GetAllowedAsSelect2ObjectAsync());
            viewModel.CatalogListJson = JsonConvert.SerializeObject(await _catalogService.GetAsSelect2ObjectAsync());
            viewModel.GuaranteeList = await _guaranteeService.GetAsSelectListAsync();
            viewModel.ColorList = EnumHelper.CastToSelectListItems<ColorType>();

            return viewModel;
        }

        public async Task<ProductBulkEditModel> PrepareBulkEditModelAsync()
        {
            var request = new ProductSearchModel
            {
                CreatedById = _webContextManager.CurrentUserId,
                PageSize = PageSize.All
            };
            var products = await _productService.GetByRequestAsync(request);
            var productBulks = _mapper.Map<IList<ProductBulkModel>>(products);

            var viewModel = new ProductBulkEditModel
            {
                CategoryListJson = JsonConvert.SerializeObject(await _categoryService.GetAllowedAsSelect2ObjectAsync()),
                CatalogListJson = JsonConvert.SerializeObject(await _catalogService.GetAsSelect2ObjectAsync()),
                GuaranteeList = await _guaranteeService.GetAsSelectListAsync(),
                ColorList = EnumHelper.CastToSelectListItems<ColorType>(),
                ProductBulks = productBulks
            };

            return viewModel;
        }

        public async Task<ProductCreateModel> PrepareCreateModelAsync(ProductCreateModel modelPrepare = null)
        {
            var viewModel = modelPrepare ?? new ProductCreateModel();

            var tags = await _tagService.GetActiveAsync();
            viewModel.Tags = _mapper.Map<List<TagModel>>(tags);
            var company = await _companyService.FindByUserIdAsync(_webContextManager.CurrentUserId);
            viewModel.CategoryId = company.CategoryId.GetValueOrDefault();
            viewModel.SellTypeList = EnumHelper.CastToSelectListItems<SellType>(); 
            viewModel.KeywordList = await _keywordService.GetAllActiveAsSelectListAsync();

            return viewModel;
        }

        public async Task<ProductDetailModel> PrepareDetailModelAsync(string productCode)
        {
            var result = await _productService.IsApprovedAsync(productCode);
            if (!result)
                throw new FactoryException("عدم تائید محصول");

            var product = await _productService.FindByCodeAsync(productCode);
            if (product == null)
                throw new FactoryException();
            var viewModel = _mapper.Map<ProductDetailModel>(product);

            //  VISIT LOG
            await _productVisitService.CreateByProductIdAsync(viewModel.Id);

            //  CATALOG DETAIL
            if (product.IsCatalog == true)
            {
                var catalogProductRequest = new ProductSearchModel
                {
                    Code = product.Code
                };
                var catalogProducts = await _productService.GetByRequestAsync(catalogProductRequest);
                viewModel.CatalogProducts = _mapper.Map<IList<CatalogDetailModel>>(catalogProducts);

                foreach (var catalogProduct in viewModel.CatalogProducts)
                {
                    catalogProduct.ProductIsExist = await _bagService.IsExistByProductIdAsync(catalogProduct.ProductId, _webContextManager.CurrentUserId);
                }

                viewModel.HighestPrice = await _productService.MaxByRequestAsync(new ProductSearchModel { CatalogId = product.CatalogId, Sell = SellType.Available, AvailableCountGreater = 0 }, p => p.Price);
                viewModel.LowestPrice = await _productService.MinByRequestAsync(new ProductSearchModel { CatalogId = product.CatalogId, Sell = SellType.Available, AvailableCountGreater = 0 }, p => p.Price);
                viewModel.CatalogCompanyCount = await _productService.CountByRequestAsync(new ProductSearchModel { DistinctByCompanyId =  true,CatalogId = product.CatalogId, Sell = SellType.Available, AvailableCountGreater = 0 });
               }

            //  OTHER
            viewModel.LikeCount = await _productLikeService.CountAllLikedByProductIdAsync(viewModel.Id);
            viewModel.ImageCount = await _productImageService.CountAllByProductIdAsync(viewModel.Id);
            viewModel.TagCount = await _productTagService.CountAllByProductIdAsync(viewModel.Id);
            viewModel.InitLike = await _productLikeService.IsLikeCurrentUserByProductIdAsync(viewModel.Id);
            viewModel.InitNotify = await _productNotifyService.IsExistByProductIdAsync(viewModel.Id, true);
            viewModel.VisitCount = await _productVisitService.CountByProductIdAsync(viewModel.Id);
            viewModel.IsExist = await _bagService.IsExistByProductIdAsync( viewModel.Id,_webContextManager.CurrentUserId);
            viewModel.RateUsers = await _productRateService.GetUserCountByProductIdAsync(viewModel.Id);
            viewModel.RateNumber = await _productRateService.GetRateByProductIdAsync(viewModel.Id);
            viewModel.CurrentUserRate = await _productRateService.GetRateByCurrentUserAsync(viewModel.Id);

            //  CATEGORY OPTION
            if (product.CategoryId != null)
            {
                var categoryOption = await _categoryService.GetCategoryOptionByIdAsync(product.CategoryId.Value);
                viewModel.CategoryOption = _mapper.Map<CategoryOptionModel>(categoryOption);
            }

            //  PRODUCT IMAGES
            if (viewModel.IsCatalog == true)
            {
                var catalogImages = await _catalogImageService.GetByCatalogIdAsync(viewModel.CatalogId.Value);
                viewModel.Images = _mapper.Map<List<ProductImageModel>>(catalogImages);
            }
            else
            {
                var productImages = await _productImageService.GetByProductIdAsync(viewModel.Id);
                viewModel.Images = _mapper.Map<List<ProductImageModel>>(productImages);
            }

            //  PRODUCT FEATURES
            if (viewModel.IsCatalog == true)
            {
                var catalogFeatureRequest = new CatalogFeatureSearchModel
                {
                    CatalogId = viewModel.CatalogId,
                    PageSize = PageSize.Count100
                };
                var catalogFeatures = await _catalogFeatureService.GetByRequestAsync(catalogFeatureRequest);
                viewModel.Features = _mapper.Map<List<ProductFeatureModel>>(catalogFeatures);
            }
            else
            {
                var requestFeature = new ProductFeatureSearchModel
                {
                    ProductId = viewModel.Id,
                    PageSize = PageSize.Count100
                };
                var features = await _productFeatureService.GetByRequestAsync(requestFeature);
                viewModel.Features = _mapper.Map<List<ProductFeatureModel>>(features);
            }

            //  PRODUCT TAGS
            var tags = await _productTagService.GetByProductIdAsync(viewModel.Id);
            viewModel.Tags = tags.Select(model => new ProductTagModel
            {
                TagTitle = model.Tag.Title,
                TagColor = model.Tag.Color
            }).ToList();

            //  PRODUCT COMMENTS
            var request = new ProductCommentSearchModel
            {
                PageSize = PageSize.Count100,
                ProductId = viewModel.Id,
                State = StateType.Approved
            };
            var productCommentList = await _productCommentService.ListByRequestAsync(request);
            viewModel.ProductCommentList = productCommentList;

            //  PRODUCT SPECIFICATIONS
            if (viewModel.IsCatalog == true)
            {
                var catalogSpecification = await _catalogSpecificationService.GetByCatalogIdAsync(viewModel.CatalogId.Value);
                var orderProductSpecificationViewModels = catalogSpecification.Select(model =>
                        new ProductSpecificationModel
                        {
                            ProductId = model.CatalogId.GetValueOrDefault(),
                            Id = model.Id,
                            SpecificationId = model.SpecificationId.GetValueOrDefault(),
                            SpecificationOptionTitle = model.SpecificationOption?.Title ?? model.Value,
                            SpecificationTitle = model.Specification?.Title,
                            SpecificationOrder = model.Specification?.Order
                        })
                    .OrderBy(model => model.SpecificationOrder)
                    .ThenBy(model => model.SpecificationTitle)
                    .ThenBy(model => model.SpecificationOptionTitle)
                    .ToList();

                viewModel.ProductSpecifications = orderProductSpecificationViewModels
                    .GroupBy(model => model.SpecificationTitle, model => model.SpecificationOptionTitle,
                        (key, value) => new ProductSpecificationModel
                        {
                            SpecificationTitle = key,
                            SpecificationValues = value.ToList()
                        })
                    .ToList();
            }
            else
            {
                var productSpecification = await _productSpecificationService.GetByProductIdAsync(viewModel.Id);
                var orderProductSpecificationViewModels = productSpecification.Select(model =>
                        new ProductSpecificationModel
                        {
                            ProductId = model.ProductId.GetValueOrDefault(),
                            Id = model.Id,
                            SpecificationId = model.SpecificationId.GetValueOrDefault(),
                            SpecificationOptionTitle = model.SpecificationOption?.Title ?? model.Value,
                            SpecificationTitle = model.Specification?.Title,
                            SpecificationOrder = model.Specification?.Order
                        })
                    .OrderBy(model => model.SpecificationOrder)
                    .ThenBy(model => model.SpecificationTitle)
                    .ThenBy(model => model.SpecificationOptionTitle)
                    .ToList();

                viewModel.ProductSpecifications = orderProductSpecificationViewModels
                    .GroupBy(model => model.SpecificationTitle, model => model.SpecificationOptionTitle,
                        (key, value) => new ProductSpecificationModel
                        {
                            SpecificationTitle = key,
                            SpecificationValues = value.ToList()
                        })
                    .ToList();
            }
            

            return viewModel;
        }

        public async Task<ProductEditModel> PrepareEditModelAsync(string productCode, bool isMine = false, ProductEditModel modelPrepare = null)
        {
            var result = await _productService.IsMineByCodeAsync(productCode);
            if (!result)
                throw new FactoryException("عدم دسترسی به این محصول");

            var product = await _productService.FindByCodeAsync(productCode);
            var viewModel = modelPrepare ?? _mapper.Map<ProductEditModel>(product);

            var tags = await _tagService.GetActiveAsync();
            viewModel.Tags = _mapper.Map<List<TagModel>>(tags);

            var productTags = await _productTagService.GetByProductIdAsync(product.Id);
            viewModel.ProductTags = _mapper.Map<List<ProductTagCreateModel>>(productTags);

            viewModel.SellTypeList = EnumHelper.CastToSelectListItems<SellType>();
            viewModel.KeywordList = await _keywordService.GetAllActiveAsSelectListAsync();
            viewModel.ProductKeywords = await _productKeywordService.GetTitlesByProductIdAsync(product.Id);
            viewModel.ProductKeywordList = await _productKeywordService.GetIdsByProductIdAsync(product.Id);

            if (isMine)
                viewModel.IsMine = true;

            return viewModel;
        }

        public async Task<ProductListModel> PrepareListModelAsync(ProductSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _productService.CountByRequestAsync(model);
            var products = await _productService.GetByRequestAsync(model);
            var productsViewModel = _mapper.Map<List<ProductModel>>(products);
            productsViewModel = productsViewModel.GroupBy(m => m.Code).Select(m => m.First()).ToList();
            foreach (var productViewModel in productsViewModel)
            {
                if (productViewModel.IsCatalog == true)
                {
                    productViewModel.HighestPrice = await _productService.MaxByRequestAsync(new ProductSearchModel {CatalogId = productViewModel.CatalogId, Sell = SellType.Available, AvailableCountGreater = 0 }, product => product.Price);
                    productViewModel.LowestPrice = await _productService.MinByRequestAsync(new ProductSearchModel {CatalogId = productViewModel.CatalogId, Sell = SellType.Available, AvailableCountGreater = 0 }, product => product.Price);
                    productViewModel.CatalogCompanyCount = await _productService.CountByRequestAsync(new ProductSearchModel { DistinctByCompanyId = true, CatalogId = productViewModel.CatalogId, Sell = SellType.Available, AvailableCountGreater = 0 });
                }
            }

            for (var i = 0; i < productsViewModel.Count; i++)
            {
                productsViewModel[i].IsExist = await _bagService.IsExistByProductIdAsync(productsViewModel[i].Id,_webContextManager.CurrentUserId);
            }

            var viewModel = new ProductListModel
            {
                Products = productsViewModel,
                SearchModel = model,
                PageSizeList = await _listManager.GetPageSizeListAsync(),
                SortDirectionList = await _listManager.GetSortDirectionListAsync(),
                SortMemberList = await _listManager.GetSortMemberFilterListAsync(),
                StateTypeList = EnumHelper.CastToSelectListItems<StateType>(),
                ProductApproved = await _productService.CountByUserIdAsync(_webContextManager.CurrentUserId, StateType.Approved),
                ProductPendeing = await _productService.CountByUserIdAsync(_webContextManager.CurrentUserId, StateType.Pending),
                ProductReject = await _productService.CountByUserIdAsync(_webContextManager.CurrentUserId, StateType.Rejected),
                ProductAll = await _productService.CountByUserIdAsync(_webContextManager.CurrentUserId, StateType.All)
            };

            if (isCurrentUser)
            {
                viewModel.IsMine = true;
                viewModel.Products.ForEach(p => p.IsMine = true);
            }

            return viewModel;
        }

        public async Task<ProductReviewListModel> PrepareReviewListModelAsync(Guid productId)
        {
            var productReviews = await _productReviewService.GetByProductIdAsync(productId);
            var productReviewsViewModel = _mapper.Map<IList<ProductReviewModel>>(productReviews);
            var listViewModel = new ProductReviewListModel
            {
                ProductReviews = productReviewsViewModel
            };

            return listViewModel;
        }

        public async Task<ProductSearchModel> PrepareSearchModelAsync(ProductSearchModel model)
        {
            var viewModel = new ProductSearchModel();

            //  PRODUCT LIST
           model.CategoryWithMany = true;
            model.State = StateType.Approved;
            model.Sell = SellType.Available;
            viewModel.MinPrice = await _productService.MinByRequestAsync(model, product => product.Price);
            viewModel.MaxPrice = await _productService.MaxByRequestAsync(model, product => product.Price);

            var category = await _categoryService.FindByAliasAsync(model.CategoryAlias);
            if (category != null) model.CategoryId = category.Id;
            model.SpecificationsDictionary = model.QueryString.ToQueryStringDictionary();
          
            if (model.Price.HasValue())
            {
                var priceOrigin = model.Price.Split('-');
                if (priceOrigin.Length == 2)
                {
                    decimal result;
                    if (decimal.TryParse(priceOrigin[0], out result))
                        model.MinPrice = result;
                    if (decimal.TryParse(priceOrigin[1], out result))
                        model.MaxPrice = result;
                }
            }
            if (model.SpecificationsDictionary != null && model.SpecificationsDictionary.ContainsKey("color"))
                model.Colors = model.SpecificationsDictionary["color"].Select(s => s.ToInt32()).ToList();

                var productList = await _productService.GetByRequestAsync(model);
                viewModel.Products = _mapper.Map<List<ProductModel>>(productList);
           
           //viewModel.Products = viewModel.Products.GroupBy(model => model.Code).Select(model => model.First()).ToList();
            foreach (var productViewModel in viewModel.Products)
            {
                if (productViewModel.IsCatalog == true)
                {
                    productViewModel.HighestPrice = await _productService.MaxByRequestAsync(new ProductSearchModel { CatalogId = productViewModel.CatalogId, Sell = SellType.Available, AvailableCountGreater = 0 }, product => product.Price);
                    productViewModel.LowestPrice = await _productService.MinByRequestAsync(new ProductSearchModel { CatalogId = productViewModel.CatalogId, Sell = SellType.Available, AvailableCountGreater = 0 }, product => product.Price);
                    productViewModel.CatalogCompanyCount = await _productService.CountByRequestAsync(new ProductSearchModel { DistinctByCompanyId = true, CatalogId = productViewModel.CatalogId, Sell = SellType.Available, AvailableCountGreater = 0 });
                }
            }

            //  FILL SEARCH
            viewModel.SearchModel = model;
            viewModel.SearchModel.TotalCount = await _productService.CountByRequestAsync(model);
           
            //  FILL LIST
            viewModel.CityList = await _addressService.GetProvinceAsSelectListItemAsync();
            viewModel.PageSizeFilterList = EnumHelper.CastToSelectListItems<PageSizeFilterType>();
            viewModel.SortDirectionFilterList = await _listManager.GetSortDirectionFilterListAsync();
            viewModel.SortMemberFilterList = await _listManager.GetSortMemberFilterListAsync();
            viewModel.CategoryList = await _categoryService.GetRaletedCategoriesByAliasAsync(model.CategoryAlias);

            var rootId = (await _categoryService.GetRootAsync()).Id;
            var categories = await _categoryService.GetChildsByIdAsync(rootId);
            viewModel.Categories = _mapper.Map<List<CategoryModel>>(categories);
            viewModel.Specifications = await _specificationService.GetViewModelByCategoryAliasAsync(model.CategoryAlias);
            //viewModel.Colors = EnumHelper.CastToSelectListItems<ColorType>();
            foreach (var color in viewModel.Colors)
            {
                if (model.Colors != null){
                    if (model.Colors.Contains(color.ToInt32()))
                    {
                        //color.Selected = true;
                    }
                }
            }

            foreach (var specification in viewModel.Specifications)
            {
                foreach (var specificationOption in specification.Options)
                {
                    if (model.SpecificationsDictionary != null && (model.SpecificationsDictionary.ContainsKey(specification.Title) && model.SpecificationsDictionary[specification.Title].Any(m => m == specificationOption.Title)))
                        specificationOption.IsSelected = true;
                }
            }
            viewModel.RequestValues =
                await _productService.CastQueryDictionaryToRequestValues(viewModel.SearchModel
                    .SpecificationsDictionary);
            return viewModel;
        }

        public async Task<ProductBulkEditModel> PrepareEditCatalogModelAsync(string productCode, bool isMine = false, ProductBulkEditModel modelPrepare = null)
        {
            var products = await _productService.GetByCodeWithCurrentUser(productCode);
            var productBulks = _mapper.Map<IList<ProductBulkModel>>(products);

            var viewModel = new ProductBulkEditModel
            {
                CategoryListJson = JsonConvert.SerializeObject(await _categoryService.GetAllowedAsSelect2ObjectAsync()),
                CatalogListJson = JsonConvert.SerializeObject(await _catalogService.GetAsSelect2ObjectAsync()),
                GuaranteeList = await _guaranteeService.GetAsSelectListAsync(),
                ColorList = EnumHelper.CastToSelectListItems<ColorType>(),
                ProductBulks = productBulks
            };
            //var viewModel = viewModelPrepare ?? _mapper.Map<ProductBulkEditViewModel>(product);

            //viewModel.CategoryListJson = JsonConvert.SerializeObject(await _categoryService.GetAllowedAsSelect2ObjectAsync());
            //viewModel.CatalogListJson = JsonConvert.SerializeObject(await _catalogService.GetAsSelect2ObjectAsync());
            //viewModel.GuaranteeList = await _guaranteeService.GetAsSelectListAsync();
            //viewModel.ColorList = SelectListHelper.CastToSelectListItems<ColorType>();

            if (isMine)
                viewModel.IsMine = true;

            return viewModel;
        }

        #endregion Public Methods
    }
}