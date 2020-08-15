using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Companies;
using Advertise.Core.Model.General;
using Advertise.Core.Model.Products;
using Advertise.Core.Types;
using Advertise.Service.Carts;
using Advertise.Service.Categories;
using Advertise.Service.Common;
using Advertise.Service.Companies;
using Advertise.Service.Products;
using Advertise.Service.Receipts;
using Advertise.Service.Statistics;
using Advertise.Service.Users;
using AutoMapper;

namespace Advertise.Service.Factory.Common
{
    public class CommonFactory : ICommonFactory
    {
        #region Private Fields

        private readonly ICategoryService _categoryService;
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private readonly IProductLikeService _productLikeService;
        private readonly IProductService _productService;
        private readonly IProductVisitService _productVisitService;
        private readonly IReceiptOptionService _receiptOptionService;
        private readonly IStatisticService _statisticService;
        private readonly IUserOnlineService _userOnlineService;
        private readonly IUserService _userService;
        private readonly IWebContextManager _webContextManager;
        private readonly ICompanyFollowService _companyFollowService;
        private readonly ICartService _cartService;

        #endregion Private Fields

        #region Public Constructors

        public CommonFactory(IProductService productService, IUserService userService, IMapper mapper, IStatisticService statisticService, ICompanyService companyService, ICategoryService categoryService, IProductVisitService productVisitService, IProductLikeService productLikeService, IWebContextManager webContextManager, IReceiptOptionService receiptOptionService, IUserOnlineService userOnlineService, ICompanyFollowService companyFollowService, ICartService cartService)
        {
            _productService = productService;
            _userService = userService;
            _mapper = mapper;
            _companyService = companyService;
            _categoryService = categoryService;
            _productVisitService = productVisitService;
            _productLikeService = productLikeService;
            _webContextManager = webContextManager;
            _receiptOptionService = receiptOptionService;
            _userOnlineService = userOnlineService;
            _companyFollowService = companyFollowService;
            _cartService = cartService;
            _statisticService = statisticService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<PanelBoardModel> PrepareDashBoardModelAsync()
        {
            var viewModel = new PanelBoardModel
            {
                AllVisitCount = await _statisticService.CountAllAsync(),
                AllCategoryCount = await _categoryService.CountAllAsync(),
                AllCompanyCount = await _companyService.CountAllAsync(),
                AllUserCount = await _userService.CountAllAsync(),
                AllProductCount = await _productService.CountAllAsync(),
                AllUserOnlineCount = await _userOnlineService.CountAllAsync(),
                ApprovedProductCount = await _productService.CountByStateAsync(StateType.Approved),
                RejectedProductCount = await _productService.CountByStateAsync(StateType.Rejected),
                PendingProductCount = await _productService.CountByStateAsync(StateType.Pending),
                ApprovedCompanyCount = await _companyService.CountByStateAsync(StateType.Approved),
                RejectedCompanyCount = await _companyService.CountByStateAsync(StateType.Rejected),
                PendingCompanyCount = await _companyService.CountByStateAsync(StateType.Pending),
                ServerTime = DateTime.Now
            };

            return viewModel;
        }

        public async Task<PanelBoardModel> PrepareDashBoardModelAsync(Guid userId)
        {
            var user = await _userService.FindByIdAsync(userId);
            var viewModel = _mapper.Map<PanelBoardModel>(user);
            viewModel.ProductApprovedCount = await _productService.CountByUserIdAsync(userId, StateType.Approved);
            viewModel.ProductPendingCount = await _productService.CountByUserIdAsync(userId, StateType.Pending);
            viewModel.ProductRejectCount = await _productService.CountByUserIdAsync(userId, StateType.Rejected);

            return viewModel;
        }

        public async Task<LandingPageModel> PrepareLandingPageModelAsync()
        {
            var viewModel = new LandingPageModel();

            //  Last Products
            //var lastProductRequest = new ProductSearchRequest
            //{
            //    PageSize = 15,
            //    State = StateType.ApproveAjax
            //};
            //var lastProducts = await _productService.GetByRequestAsync(lastProductRequest);
            //viewModel.LastProductItemList = _mapper.Map<IList<ProductViewModel>>(lastProducts);

            //  Last Mobile Products
            var lastMobileProductRequest = new ProductSearchModel
            {
                PageSize = 60,
                State = StateType.Approved,
                CategoryAlias = "Category-Digital-Cellphone"
            };
            var lastMobileProducts = await _productService.GetByRequestAsync(lastMobileProductRequest);
            viewModel.LastMobileProductItemList = _mapper.Map<IList<ProductModel>>(lastMobileProducts);
            viewModel.LastMobileProductItemList = viewModel.LastMobileProductItemList.GroupBy(model => model.Code).Select(model => model.First()).ToList();
            foreach (var productViewModel in viewModel.LastMobileProductItemList)
            {
                if (productViewModel.IsCatalog == true)
                {
                    productViewModel.HighestPrice = await _productService.MaxByRequestAsync(new ProductSearchModel { CatalogId = productViewModel.CatalogId, Sell = SellType.Available, AvailableCountGreater = 0 }, product => product.Price);
                    productViewModel.LowestPrice = await _productService.MinByRequestAsync(new ProductSearchModel { CatalogId = productViewModel.CatalogId, Sell = SellType.Available, AvailableCountGreater = 0 }, product => product.Price);
                    productViewModel.CatalogCompanyCount = await _productService.CountByRequestAsync(new ProductSearchModel { DistinctByCompanyId = true, CatalogId = productViewModel.CatalogId, Sell = SellType.Available, AvailableCountGreater = 0 });
                }
            }

            //  Most Visit Product
            //var mostVisitProductRequest = new ProductSearchRequest
            //{
            //    PageSize = 15,
            //    State = StateType.ApproveAjax,
            //    Ids = await _productVisitService.GetMostVisitProductIdAsync()
            //};
            //var mostVisitProduct = await _productService.GetByRequestAsync(mostVisitProductRequest);
            //viewModel.MostVisitedItemList = _mapper.Map<IList<ProductViewModel>>(mostVisitProduct);

            //  Most Like Product
            //var mostLikeProductRequest = new ProductSearchRequest
            //{
            //    PageSize = 15,
            //    State = StateType.ApproveAjax,
            //    Ids = await _productLikeService.GetMostLikedProductIdAsync()
            //};
            //var mostLikeProduct = await _productService.GetByRequestAsync(mostLikeProductRequest);
            //viewModel.MostLikedItemList = _mapper.Map<IList<ProductViewModel>>(mostLikeProduct);

            //  My Last Visit Product
            var myLastVisitProductRequest = new ProductSearchModel
            {
                PageSize = 15,
                State = StateType.Approved,
                Ids = await _productVisitService.GetLastProductIdByCurrentUserAsync()
            };
            var myLastVisitProduct = await _productService.GetByRequestAsync(myLastVisitProductRequest);
            viewModel.MyLastVisitItemList = _mapper.Map<IList<ProductModel>>(myLastVisitProduct);
            viewModel.MyLastVisitItemList = viewModel.MyLastVisitItemList.GroupBy(model => model.Code).Select(model => model.First()).ToList();
            foreach (var productViewModel in viewModel.MyLastVisitItemList)
            {
                productViewModel.IsExist = await _cartService.IsExistByProductIdAsync(productViewModel.Id, _webContextManager.CurrentUserId);
                if (productViewModel.IsCatalog == true)
                {
                    productViewModel.HighestPrice = await _productService.MaxByRequestAsync(new ProductSearchModel { CatalogId = productViewModel.CatalogId, Sell = SellType.Available, AvailableCountGreater = 0 }, product => product.Price);
                    productViewModel.LowestPrice = await _productService.MinByRequestAsync(new ProductSearchModel { CatalogId = productViewModel.CatalogId, Sell = SellType.Available, AvailableCountGreater = 0 }, product => product.Price);
                    productViewModel.CatalogCompanyCount = await _productService.CountByRequestAsync(new ProductSearchModel { DistinctByCompanyId = true, CatalogId = productViewModel.CatalogId, Sell = SellType.Available, AvailableCountGreater = 0 });
                }
            }

            //  Last Company
            //var lastCompanyRequest = new CompanySearchRequest
            //{
            //    PageSize = 30,
            //    State = StateType.ApproveAjax
            //};
            //var lastCompany = await _companyService.GetByRequestAsync(lastCompanyRequest);
            //var listCompany = _mapper.Map<IList<CompanyViewModel>>(lastCompany);
            //await PrepareCompanyViewModelAsync(listCompany);
            //viewModel.LastCompanyItemList = listCompany;

            //  Last Mobile Company
            var lastMobileCompanyRequest = new CompanySearchModel
            {
                PageSize = 30,
                State = StateType.Approved,
                CategoryId = (await _categoryService.FindByAliasAsync("Category-Digital-Cellphone")).Id
            };
            var lastMobileCompany = await _companyService.GetByRequestAsync(lastMobileCompanyRequest);
            var listMobileCompany = _mapper.Map<IList<CompanyModel>>(lastMobileCompany);
            await PrepareCompanyModelAsync(listMobileCompany);
            viewModel.LastMobileCompanyItemList = listMobileCompany;

            return viewModel;
        }

        public async Task PrepareCompanyModelAsync(IList<CompanyModel> listCompany)
        {
            foreach (var item in listCompany)
            {
                var companyFollowSearchRequest = new CompanyFollowSearchModel
                {
                    CompanyId = item.Id,
                    IsFollow = true
                };
                var followerCount = await _companyFollowService.CountByRequestAsync(companyFollowSearchRequest);
                item.FollowerCount = followerCount;
                item.InitFollow = await _companyFollowService.IsFollowByCurrentUserAsync(item.Id);
                item.ProductCount = await _productService.CountByCompanyIdAsync(item.Id, StateType.Approved);
                var userMeta = await _userService.GetUserMetaByIdAsync(item.CreatedById);
                if (userMeta.FirstName != null || userMeta.LastName != null)
                {
                    item.UserDisplayName = userMeta.FullName;
                }
                else
                {
                    var user = await _userService.FindByIdAsync(item.CreatedById);
                    item.UserDisplayName = user.UserName;
                }
            }
        }

        public async Task<ProfileModel> PrepareProfileViewModelAsync()
        {
            var user = await _userService.FindByUserIdAsync(_webContextManager.CurrentUserId);
            var viewModel = _mapper.Map<ProfileModel>(user);
            viewModel.IsSetCompanyAlias = await _companyService.HasAliasByCurrentUserAsync();
            viewModel.IsSetUsername = !user.UserName.StartsWith(CodeConst.MyApp);
            viewModel.ProductApprovedCount = await _productService.CountByUserIdAsync(_webContextManager.CurrentUserId, StateType.Approved);
            viewModel.ProductPendingCount = await _productService.CountByUserIdAsync(_webContextManager.CurrentUserId, StateType.Pending);
            viewModel.ProductRejectCount = await _productService.CountByUserIdAsync(_webContextManager.CurrentUserId, StateType.Rejected);
            viewModel.ReceiptSum = _receiptOptionService.GetSumTotalPriceAsync(_webContextManager.CurrentUserId).GetValueOrDefault();
            viewModel.FollowersCount = await _companyService.GetCountMyFollowAsync();

            return viewModel;
        }

        #endregion Public Methods
    }
}