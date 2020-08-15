using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Exceptions;
using Advertise.Core.Helpers;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Categories;
using Advertise.Core.Model.Companies;
using Advertise.Core.Model.General;
using Advertise.Core.Model.Locations;
using Advertise.Core.Model.Products;
using Advertise.Core.Types;
using Advertise.Service.Categories;
using Advertise.Service.Common;
using Advertise.Service.Companies;
using Advertise.Service.Locations;
using Advertise.Service.Products;
using Advertise.Service.Users;
using AutoMapper;

namespace Advertise.Service.Factory.Companies
{
    public class CompanyFactory : ICompanyFactory
    {
        #region Private Fields

        private readonly ILocationService _addressService;
        private readonly ICategoryService _categoryService;
        private readonly ICommonService _commonService;
        private readonly ICompanyAttachmentService _companyAttachmentService;
        private readonly ICompanyConversationService _companyConversationService;
        private readonly ICompanyFollowService _companyFollowService;
        private readonly ICompanyImageService _companyImageService;
        private readonly ICompanyQuestionService _companyQuestionService;
        private readonly ICompanyReviewService _companyReviewService;
        private readonly ICompanyService _companyService;
        private readonly ICompanySocialService _companySocialService;
        private readonly ICompanyTagService _companyTagService;
        private readonly ICompanyVideoService _companyVideoService;
        private readonly ICompanyVisitService _companyVisitService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public CompanyFactory(ICompanyService companyService, IMapper mapper, IWebContextManager webContextManager, ICompanyVisitService companyVisitService, ICommonService commonService, ICompanyFollowService companyFollowService, ICompanyImageService companyImageService, ICompanyTagService companyTagService, ICategoryService categoryService, IProductService productService, IUserService userService, ICompanySocialService companySocialService, ICompanyConversationService companyConversationService, ILocationService addressService, ICompanyReviewService companyReviewService, ICompanyAttachmentService companyAttachmentService, ICompanyVideoService companyVideoService, ICompanyQuestionService companyQuestionService, IListService listService)
        {
            _companyService = companyService;
            _mapper = mapper;
            _webContextManager = webContextManager;
            _companyVisitService = companyVisitService;
            _commonService = commonService;
            _companyFollowService = companyFollowService;
            _companyImageService = companyImageService;
            _companyTagService = companyTagService;
            _categoryService = categoryService;
            _productService = productService;
            _userService = userService;
            _companySocialService = companySocialService;
            _companyConversationService = companyConversationService;
            _addressService = addressService;
            _companyReviewService = companyReviewService;
            _companyAttachmentService = companyAttachmentService;
            _companyVideoService = companyVideoService;
            _companyQuestionService = companyQuestionService;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<CompanyDetailInfoModel> PrepareDetailInfoModelAsync(string companyAlias)
        {
            var company = await _companyService.FindByAliasAsync(companyAlias);
            if (company == null)
                throw new FactoryException();
            var viewModel = _mapper.Map<CompanyDetailInfoModel>(company);

            //viewModel.Address = company.Address?.Extra;

            if (company.CreatedById != null)
            {
                var social = await _companySocialService.FindByUserIdAsync(company.CreatedById.Value);
                if (social != null)
                {
                    viewModel.FacebookLink = social.FacebookLink;
                    viewModel.GooglePlusLink = social.GooglePlusLink;
                    viewModel.TwitterLink = social.TwitterLink;
                    viewModel.YoutubeLink = social.YoutubeLink;
                    viewModel.TelegramLink = social.TelegramLink;
                    viewModel.InstagramLink = social.InstagramLink;
                }
            }

            return viewModel;
        }

        public async Task<CompanyDetailModel> PrepareDetailModelAsync(string companyAlias, string slug)
        {
            var isExist = await _companyService.IsExistByAliasAsync(companyAlias);
            if (!isExist)
                throw new FactoryException("کمپانی وجود ندارد");

            var result = await _companyService.IsApprovedByAliasAsync(companyAlias);
            if (!result)
                throw new FactoryException("کمپانی تایید نشده است");

            var slugResult = await _companyService.CompareNameAndSlugAsync(companyAlias, slug);
            if (!slugResult)
                throw new FactoryException("عدم تطابق اسلاگ");

            var company = await _companyService.FindByAliasAsync(companyAlias);
            if (company == null)
                throw new FactoryException();
            var viewModel = _mapper.Map<CompanyDetailModel>(company);

            await _companyVisitService.CreateByCompanyIdAsync(company.Id);

            //  COMPANY COUNTS
            viewModel.ImageCount = await _companyImageService.CountAllByCompanyIdAsync(viewModel.Id);
            viewModel.FollowerCount = await _companyFollowService.CountAllFollowByCompanyIdAsync(viewModel.Id);
            viewModel.ProductCount = await _productService.CountByCompanyIdAsync(viewModel.Id);
            viewModel.TagCount = await _companyTagService.CountAllTagByCompanyIdAsync(viewModel.Id);
            viewModel.VisitCount = await _companyVisitService.CountByCompanyIdAsync(viewModel.Id);

            //  USER DETAILS
            var user = await _userService.FindByUserIdAsync(viewModel.CreatedById);
            viewModel.FullName = user.Meta.FullName ?? "";
            viewModel.UserEmail = user.Email;
            viewModel.UserUserName = user.UserName;

            //  OTHER
            viewModel.CategoryTitle = (await _categoryService.FindByIdAsync(viewModel.CategoryId)).Title;
            viewModel.InitFollow = await _companyFollowService.IsFollowByCurrentUserAsync(viewModel.Id);

            //  CATEGORY OPTION
            if (company.CategoryId != null)
            {
                var categoryOption = await _categoryService.GetCategoryOptionByIdAsync(company.CategoryId.Value);
                viewModel.CategoryOption = _mapper.Map<CategoryOptionModel>(categoryOption);
            }

            //  COMPANY CONVERSATIONS
            viewModel.ConversationList = await _companyConversationService.GetListByUserIdAsync(viewModel.CreatedById);
            
            //  COMPANY IMAGES
            var images = await _companyImageService.GetApprovedsByCompanyIdAsync(viewModel.Id);
            viewModel.ImageList = _mapper.Map<IList<CompanyImageModel>>(images);
           
            //  COMPANY PRODUCTS
            var products = await _productService.GetApprovedByCompanyIdAsync(viewModel.Id);
            viewModel.ProductList = new ProductListModel
            {
                Products = _mapper.Map<List<ProductModel>>(products)
            };
            viewModel.ProductList.Products = viewModel.ProductList.Products.GroupBy(model => model.Code).Select(model => model.First()).ToList();
            foreach (var productViewModel in viewModel.ProductList.Products)
            {
                if (productViewModel.IsCatalog == true)
                {
                    productViewModel.HighestPrice = await _productService.MaxByRequestAsync(new ProductSearchModel { CatalogId = productViewModel.CatalogId, Sell = SellType.Available, AvailableCountGreater = 0 }, product => product.Price);
                    productViewModel.LowestPrice = await _productService.MinByRequestAsync(new ProductSearchModel { CatalogId = productViewModel.CatalogId, Sell = SellType.Available, AvailableCountGreater = 0 }, product => product.Price);
                    productViewModel.CatalogCompanyCount = await _productService.CountByRequestAsync(new ProductSearchModel {DistinctByCompanyId = true,CatalogId = productViewModel.CatalogId, Sell = SellType.Available, AvailableCountGreater = 0 });
                }
            }

            //  COMPANY ATTACHMENTS
            var companyAttachments = await _companyAttachmentService.GetApprovedByCompanyIdAsync(viewModel.Id);
            viewModel.AttachmentList = new CompanyAttachmentListModel
            {
                CompanyAttachments = _mapper.Map<IList<CompanyAttachmentModel>>(companyAttachments)
            };

            //  COMPANY REVIEWS
            var companyReview = await _companyReviewService.GetByCompanyIdAsync(viewModel.Id);
            viewModel.ReviewList = new CompanyReviewListModel
            {
                CompanyReviews = _mapper.Map<IList<CompanyReviewModel>>(companyReview)
            };

            //  COMPANY VIDEOS
            var companyVideo = await _companyVideoService.GetApprovedByCompanyIdAsync(viewModel.Id);
            viewModel.VideoList = _mapper.Map<IList<CompanyVideoModel>>(companyVideo);
           
            //  COMPANY QUESTIONS
            var companyQuestions = await _companyQuestionService.GetAllByCompanyIdAsync(viewModel.Id);
            var viewModelQuestion = _mapper.Map<IList<CompanyQuestionModel>>(companyQuestions);
            viewModel.QuestionList = new CompanyQuestionListModel
            {
                CompanyQuestions = viewModelQuestion
            };

            //  IS MY SELF
            viewModel.IsMyself = await _companyService.IsMySelfAsync(viewModel.Id);
            viewModelQuestion.ForEach(p => p.IsMyself = viewModel.IsMyself);

            return viewModel;
        }

        public async Task<CompanyEditModel> PrepareEditModelAsync(string companyAlias = null, bool applyCurrentUser = false, CompanyEditModel modelApply = null)
        {
            var company = await _companyService.FindByAliasAsync(companyAlias);
            if (companyAlias == null)
                company = await _companyService.FindByUserIdAsync(_webContextManager.CurrentUserId);

            var viewModel = modelApply ?? _mapper.Map<CompanyEditModel>(company);

            var address = await _companyService.GetAddressViewModelByIdAsync(viewModel.Id);
            if (address != null)
            {
                viewModel.Location = _mapper.Map<LocationModel>(address);
                if (viewModel.Location.LocationCity == null)
                    viewModel.Location.LocationCity = new LocationCityModel();
            }
            else
            {
                viewModel.Location = new LocationModel
                {
                    LocationCity = new LocationCityModel()
                };
            }
            viewModel.CategoryRoot = await _categoryService.IsRootAsync(viewModel.CategoryId);
            viewModel.IsSetAlias = await _companyService.IsExistAliasByIdAsync(viewModel.Id);
            viewModel.AddressProvince = await _addressService.GetProvinceAsSelectListItemAsync();
            viewModel.ClearingList = EnumHelper.CastToSelectListItems<ClearingType>();
            viewModel.EmployeeRangeList = EnumHelper.CastToSelectListItems<EmployeeRangeType>();

            if (applyCurrentUser)
                viewModel.IsMine = true;

            return viewModel;
        }

        public async Task<CompanyImageListModel> PrepareImageListViewModelAsync(Guid companyId)
        {
            var companyImages = await _companyImageService.GetApprovedsByCompanyIdAsync(companyId);
            var companyImagesViewModel = _mapper.Map<IList<CompanyImageModel>>(companyImages);
            var listViewModel = new CompanyImageListModel
            {
                CompanyImages = companyImagesViewModel
            };

            return listViewModel;
        }

        public async Task<CompanyListModel> PrepareListModelAsync(CompanySearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _companyService.CountByRequestAsync(model);
            var companies = await _companyService.GetByRequestAsync(model);
            var companyViewModel = _mapper.Map<IList<CompanyModel>>(companies);

            foreach (var viewModel in companyViewModel)
            {
                viewModel.ProductCount = await _productService.CountByCompanyIdAsync(viewModel.Id, StateType.Approved);
                viewModel.InitFollow = await _companyFollowService.IsFollowByCurrentUserAsync(viewModel.Id);
            }

            var companyList = new CompanyListModel
            {
                SearchModel = model,
                Companies = companyViewModel,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync(),
                SortMemberList = await _listService.GetSortMemberListByTitleAsync(),
                StateList = EnumHelper.CastToSelectListItems<StateType>()
            };

            if (isCurrentUser)
                companyList.Companies.ForEach(p => p.IsMine = true);

            return companyList;
        }

        public async Task<ProductListModel> PrepareProductListModelAsync(Guid companyId)
        {
            var request = new ProductSearchModel
            {
                CompanyId = companyId,
                State = StateType.Approved
            };
            var companyProducts = await _productService.GetByRequestAsync(request);
            var companyProductViewModel = _mapper.Map<IList<ProductModel>>(companyProducts);
            var listViewModel = new ProductListModel
            {
                Products = companyProductViewModel,
                SearchModel = request
            };

            return listViewModel;
        }

        public async Task<ProfileMenuModel> PrepareProfileMenuModelAsync()
        {
            var company = await _companyService.FindByUserIdAsync(_webContextManager.CurrentUserId);
            var viewModel = _mapper.Map<ProfileMenuModel>(company);

            if (company.CategoryId != null)
            {
                var categoryOption = await _categoryService.GetCategoryOptionByIdAsync(company.CategoryId.Value);
                viewModel.CategoryOption = _mapper.Map<CategoryOptionModel>(categoryOption);
            }

            return viewModel;
        }

        public async Task<CompanyReviewListModel> PrepareReviewListModelAsync(Guid companyId)
        {
            var companyReviews = await _companyReviewService.GetByCompanyIdAsync(companyId);
            var companyReviewViewModel = _mapper.Map<IList<CompanyReviewModel>>(companyReviews);
            var listViewModel = new CompanyReviewListModel
            {
                CompanyReviews = companyReviewViewModel
            };

            return listViewModel;
        }

        #endregion Public Methods
    }
}