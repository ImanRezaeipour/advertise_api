namespace Advertise.Service.Services.Permissions
{
    public static class PermissionConst
    {
        #region Public Fields

        public const string CanRoot = nameof(CanRoot);

        #region UserController

        public const string CanRootUser = nameof(CanRootUser);
        public const string CanUserEdit = nameof(CanUserEdit);
        public const string CanUserEasyRegister = nameof(CanUserEasyRegister);
        public const string CanUserDelete = nameof(CanUserDelete);
        public const string CanUserList = nameof(CanUserList);
        public const string CanUserMyEdit = nameof(CanUserMyEdit);

        #endregion UserController

        #region AccountController

        public const string CanRootAccount = nameof(CanRootAccount);
        public const string CanAccountSignOut = nameof(CanAccountSignOut);
        public const string CanAccountIsExistEmailAjax = nameof(CanAccountIsExistEmailAjax);
        public const string CanAccountIsExistUserNameAjax = nameof(CanAccountIsExistUserNameAjax);
        public const string CanAccountChangePassword = nameof(CanAccountChangePassword);
        public const string CanAccountSetPassword = nameof(CanAccountSetPassword);

        #endregion AccountController

        #region ReceiptController

        public const string CanRootReceipt = nameof(CanRootReceipt);
        public const string CanReceiptList = nameof(CanReceiptList);
        public const string CanReceiptMyList = nameof(CanReceiptMyList);

        #endregion ReceiptController

        #region ReceiptOptionController

        public const string CanRootReceiptOption = nameof(CanRootReceiptOption);
        public const string CanReceiptOptionList = nameof(CanReceiptOptionList);
        public const string CanReceiptOptionMySaleList = nameof(CanReceiptOptionMySaleList);
        public const string CanReceiptOptionMyBuyList = nameof(CanReceiptOptionMyBuyList);

        #endregion ReceiptOptionController

        #region ProfileController

        public const string CanRootProfile = nameof(CanRootProfile);

        #endregion ProfileController

        #region UserBudgetController

        public const string CanRootUserBudget = nameof(CanRootUserBudget);
        public const string CanUserBudgetDetail = nameof(CanUserBudgetDetail);

        #endregion UserBudgetController

        #region BagController

        public const string CanRootBag = nameof(CanRootBag);
        public const string CanBagMyList = nameof(CanBagMyList);
        public const string CanBagList = nameof(CanBagList);
        public const string CanBagToggleAjax = nameof(CanBagToggleAjax);
        public const string CanBagDeleteAjax = nameof(CanBagDeleteAjax);
        public const string CanBagCreateAjax = nameof(CanBagCreateAjax);
        public const string CanBagCountAjax = nameof(CanBagCountAjax);
        public const string CanBagChangeProductCountAjax = nameof(CanBagChangeProductCountAjax);
        public const string CanBagIsExistAjax = nameof(CanBagIsExistAjax);

        #endregion BagController

        #region ComplaintController

        public const string CanRootComplaint = nameof(CanRootComplaint);
        public const string CanComplaintList = nameof(CanComplaintList);
        public const string CanComplaintDeleteAjax = nameof(CanComplaintDeleteAjax);

        #endregion ComplaintController

        #region CategoryController

        public const string CanRootCategory = nameof(CanRootCategory);
        public const string CanCategoryCreate = nameof(CanCategoryCreate);
        public const string CanCategoryDeleteAjax = nameof(CanCategoryDeleteAjax);
        public const string CanCategoryEdit = nameof(CanCategoryEdit);
        public const string CanCategoryList = nameof(CanCategoryList);
        public const string CanCategoryGetCategoriesAjax = nameof(CanCategoryGetCategoriesAjax);
        public const string CanCategoryGetSubCatergoriesWithRootAjax = nameof(CanCategoryGetSubCatergoriesWithRootAjax);

        #endregion CategoryController

        #region CategoryFollowController

        public const string CanRootCategoryFollow = nameof(CanRootCategoryFollow);
        public const string CanCategoryFollowMyIsFollowAjax = nameof(CanCategoryFollowMyIsFollowAjax);
        public const string CanCategoryFollowMyToggleFollowAjax = nameof(CanCategoryFollowMyToggleFollowAjax);

        #endregion CategoryFollowController

        #region CategoryReviewController

        public const string CanRootCategoryReview = nameof(CanRootCategoryReview);
        public const string CanCategoryReviewCreate = nameof(CanCategoryReviewCreate);
        public const string CanCategoryReviewEdit = nameof(CanCategoryReviewEdit);
        public const string CanCategoryReviewDeleteAjax = nameof(CanCategoryReviewDeleteAjax);
        public const string CanCategoryReviewList = nameof(CanCategoryReviewList);

        #endregion CategoryReviewController

        #region FileController

        public const string CanRootFile = nameof(CanRootFile);
        public const string CanFileListFromUploadAjax = nameof(CanFileListFromUploadAjax);
        public const string CanFileCreateFromUploadAjax = nameof(CanFileCreateFromUploadAjax);
        public const string CanFileSaveFromUploaderAjax = nameof(CanFileSaveFromUploaderAjax);
        public const string CanFileRemoveAjax = nameof(CanFileRemoveAjax);
        public const string CanFileGetFileFromUpload = nameof(CanFileGetFileFromUpload);
        public const string CanFileGetFileFromThumb = nameof(CanFileGetFileFromThumb);

        #endregion FileController

        #region CompanyController

        public const string CanRootCompany = nameof(CanRootCompany);
        public const string CanCompanyEdit = nameof(CanCompanyEdit);
        public const string CanCompanyList = nameof(CanCompanyList);
        public const string CanCompanyEditApprove = nameof(CanCompanyEditApprove);
        public const string CanCompanyMyEdit = nameof(CanCompanyMyEdit);

        #endregion CompanyController

        #region CompanyBalanceController

        public const string CanRootCompanyBalance = nameof(CanRootCompanyBalance);
        public const string CanCompanyBalanceEdit = nameof(CanCompanyBalanceEdit);
        public const string CanCompanyBalanceCreate = nameof(CanCompanyBalanceCreate);
        public const string CanCompanyBalanceGetFileAjax = nameof(CanCompanyBalanceGetFileAjax);
        public const string CanCompanyBalanceDeleteAjax = nameof(CanCompanyBalanceDeleteAjax);
        public const string CanCompanyBalanceList = nameof(CanCompanyBalanceList);
        public const string CanCompanyBalanceMyList = nameof(CanCompanyBalanceMyList);

        #endregion CompanyBalanceController

        #region CompanyAttachmentController

        public const string CanRootCompanyAttachment = nameof(CanRootCompanyAttachment);
        public const string CanCompanyAttachmentEdit = nameof(CanCompanyAttachmentEdit);
        public const string CanCompanyAttachmentList = nameof(CanCompanyAttachmentList);
        public const string CanCompanyAttachmentDeleteAjax = nameof(CanCompanyAttachmentDeleteAjax);
        public const string CanCompanyAttachmentCreate = nameof(CanCompanyAttachmentCreate);
        public const string CanCompanyAttachmentEditApprove = nameof(CanCompanyAttachmentEditApprove);
        public const string CanCompanyAttachmentMyList = nameof(CanCompanyAttachmentMyList);
        public const string CanCompanyAttachmentMyEdit = nameof(CanCompanyAttachmentMyEdit);
        public const string CanCompanyAttachmentMyDeleteAjax = nameof(CanCompanyAttachmentMyDeleteAjax);

        #endregion CompanyAttachmentController

        #region CompanyFollowController

        public const string CanRootCompanyFollow = nameof(CanRootCompanyFollow);
        public const string CanCompanyFollowMyIsFollowAjax = nameof(CanCompanyFollowMyIsFollowAjax);
        public const string CanCompanyFollowMyToggleFollowAjax = nameof(CanCompanyFollowMyToggleFollowAjax);
        public const string CanCompanyFollowMyFollowList = nameof(CanCompanyFollowMyFollowList);

        #endregion CompanyFollowController

        #region CompanyImageController

        public const string CanRootCompanyImage = nameof(CanRootCompanyImage);
        public const string CanCompanyImageEdit = nameof(CanCompanyImageEdit);
        public const string CanCompanyImageMyEdit = nameof(CanCompanyImageMyEdit);
        public const string CanCompanyImageList = nameof(CanCompanyImageList);
        public const string CanCompanyImageDeleteAjax = nameof(CanCompanyImageDeleteAjax);
        public const string CanCompanyImageCreate = nameof(CanCompanyImageCreate);
        public const string CanCompanyImageEditApprove = nameof(CanCompanyImageEditApprove);
        public const string CanCompanyImageMyDeleteAjax = nameof(CanCompanyImageMyDeleteAjax);
        public const string CanCompanyImageMyList = nameof(CanCompanyImageMyList);

        #endregion CompanyImageController

        #region AdsController

        public const string CanRootAds = nameof(CanRootAds);
        public const string CanAdsCreate = nameof(CanAdsCreate);
        public const string CanAdsApproved = nameof(CanAdsApproved);
        public const string CanAdsEdit = nameof(CanAdsEdit);
        public const string CanAdsCreateByAdmin = nameof(CanAdsCreateByAdmin);
        public const string CanAdsEditApprove = nameof(CanAdsEditApprove);
        public const string CanAdsRejected = nameof(CanAdsRejected);

        #endregion AdsController

        #region AdsOptionController

        public const string CanRootAdsOption = nameof(CanRootAdsOption);
        public const string CanAdsOptionCreate = nameof(CanAdsOptionCreate);
        public const string CanAdsOptionEdit = nameof(CanAdsOptionEdit);
        public const string CanAdsOptionGetOptionAjax = nameof(CanAdsOptionGetOptionAjax);
        public const string CanAdsOptionGetPriceAjax = nameof(CanAdsOptionGetPriceAjax);
        public const string CanAdsOptionList = nameof(CanAdsOptionList);

        #endregion AdsOptionController

        #region AdsPaymentController

        public const string CanRootAdsPayment = nameof(CanRootAdsPayment);
        public const string CanAdsPaymentList = nameof(CanAdsPaymentList);
        public const string CanAdsPaymentMyList = nameof(CanAdsPaymentMyList);

        #endregion AdsPaymentController

        #region ManufacturerController
        public const string CanRootManufacturer = nameof(CanRootManufacturer);
        public const string CanManufacturerCreate = nameof(CanManufacturerCreate);
        public const string CanManufacturerEdit= nameof(CanManufacturerEdit);
        public const string CanManufacturerDeleteAjax= nameof(CanManufacturerDeleteAjax);
        public const string CanManufacturerList= nameof(CanManufacturerList);
        #endregion ManufacturerController

        #region GuaranteeController
        public const string CanRootGuarantee = nameof(CanRootGuarantee);
        public const string CanGuaranteeCreate = nameof(CanGuaranteeCreate);
        public const string CanGuaranteeEdit = nameof(CanGuaranteeEdit);
        public const string CanGuaranteeDeleteAjax = nameof(CanGuaranteeDeleteAjax);
        public const string CanGuaranteeList = nameof(CanGuaranteeList);
        #endregion GuaranteeController
        
        #region GuaranteeController
        public const string CanRootCatalog = nameof(CanRootCatalog);
        public const string CanCatalogCreate = nameof(CanCatalogCreate);
        public const string CanCatalogEdit = nameof(CanCatalogEdit);
        public const string CanCatalogDeleteAjax = nameof(CanCatalogDeleteAjax);
        public const string CanCatalogList = nameof(CanCatalogList);
        #endregion GuaranteeController

        #region CategorySlideController

        public const string CanRootCategorySlide = nameof(CanRootCategorySlide);
        public const string CanCategorySlideApproved = nameof(CanCategorySlideApproved);
        public const string CanCategorySlideRejected = nameof(CanCategorySlideRejected);

        #endregion CategorySlideController

        #region CategorySlideOptionController

        public const string CanRootCategorySlideOption = nameof(CanRootCategorySlideOption);
        public const string CanCategorySlideOptionCreate = nameof(CanCategorySlideOptionCreate);
        public const string CanCategorySlideOptionEdit = nameof(CanCategorySlideOptionEdit);
        public const string CanCategorySlideOptionList = nameof(CanCategorySlideOptionList);

        #endregion CategorySlideOptionController

        #region CategorySlidePaymentController

        public const string CanRootCategorySlidePayment = nameof(CanRootCategorySlidePayment);
        public const string CanCategorySlidePaymentList = nameof(CanCategorySlidePaymentList);
        public const string CanCategorySlidePaymentMyList = nameof(CanCategorySlidePaymentMyList);

        #endregion CategorySlidePaymentController

        #region CompanyVideoController

        public const string CanRootCompanyVideo = nameof(CanRootCompanyVideo);
        public const string CanCompanyVideoEdit = nameof(CanCompanyVideoEdit);
        public const string CanCompanyVideoCreate = nameof(CanCompanyVideoCreate);
        public const string CanCompanyVideoList = nameof(CanCompanyVideoList);
        public const string CanCompanyVideoDeleteAjax = nameof(CanCompanyVideoDeleteAjax);
        public const string CanCompanyVideoEditApprove = nameof(CanCompanyVideoEditApprove);
        public const string CanCompanyVideoEditReject = nameof(CanCompanyVideoEditReject);
        public const string CanCompanyVideoMyEdit = nameof(CanCompanyVideoMyEdit);
        public const string CanCompanyVideoMyList = nameof(CanCompanyVideoMyList);
        public const string CanCompanyVideoMyDeleteAjax = nameof(CanCompanyVideoMyDeleteAjax);

        #endregion CompanyVideoController

        #region CompanyQustionController

        public const string CanRootCompanyQuestion = nameof(CanRootCompanyQuestion);
        public const string CanCompanyQustionEdit = nameof(CanCompanyQustionEdit);
        public const string CanCompanyQustionList = nameof(CanCompanyQustionList);
        public const string CanCompanyQustionDeleteAjax = nameof(CanCompanyQustionDeleteAjax);
        public const string CanCompanyQustionCreate = nameof(CanCompanyQustionCreate);
        public const string CanCompanyQustionMyList = nameof(CanCompanyQustionMyList);
        public const string CanCompanyQustionEditReject = nameof(CanCompanyQustionEditReject);
        public const string CanCompanyQustionEditApprove = nameof(CanCompanyQustionEditApprove);

        #endregion CompanyQustionController

        #region CompanyReviewController

        public const string CanRootCompanyReview = nameof(CanRootCompanyReview);
        public const string CanCompanyReviewCreate = nameof(CanCompanyReviewCreate);
        public const string CanCompanyReviewEdit = nameof(CanCompanyReviewEdit);
        public const string CanCompanyReviewDeleteAjax = nameof(CanCompanyReviewDeleteAjax);
        public const string CanCompanyReviewList = nameof(CanCompanyReviewList);

        #endregion CompanyReviewController

        #region CompanySocialController

        public const string CanRootCompanySocial = nameof(CanRootCompanySocial);
        public const string CanCompanySocialEdit = nameof(CanCompanySocialEdit);
        public const string CanCompanySocialDeleteAjax = nameof(CanCompanySocialDeleteAjax);
        public const string CanCompanySocialList = nameof(CanCompanySocialList);
        public const string CanCompanySocialCreate = nameof(CanCompanySocialCreate);
        public const string CanCompanySocialMyEdit = nameof(CanCompanySocialMyEdit);

        #endregion CompanySocialController

        #region CompanySocialController

        public const string CanRootCompanyOfficial = nameof(CanRootCompanyOfficial);
        public const string CanCompanyOfficialEdit = nameof(CanCompanyOfficialEdit);
        public const string CanCompanyOfficialList = nameof(CanCompanyOfficialList);
        public const string CanCompanyOfficialCreate = nameof(CanCompanyOfficialCreate);
        public const string CanCompanyOfficialMyEdit = nameof(CanCompanyOfficialMyEdit);
        public const string CanCompanyOfficialApprove = nameof(CanCompanyOfficialApprove);

        #endregion CompanySocialController

        #region NewsController

        public const string CanRootNews = nameof(CanRootNews);
        public const string CanNewsCreate = nameof(CanNewsCreate);
        public const string CanNewsEdit = nameof(CanNewsEdit);
        public const string CanNewsDeleteAjax = nameof(CanNewsDeleteAjax);
        public const string CanNewsList = nameof(CanNewsList);

        #endregion NewsController

        #region NewsletterController

        public const string CanRootNewsletter = nameof(CanRootNewsletter);
        public const string CanNewsletterDeleteAjax = nameof(CanNewsletterDeleteAjax);
        public const string CanNewsletterCreate = nameof(CanNewsletterCreate);
        public const string CanNewsletterList = nameof(CanNewsletterList);

        #endregion NewsletterController

        #region TagController

        public const string CanRootTag = nameof(CanRootTag);
        public const string CanTagCreate = nameof(CanTagCreate);
        public const string CanTagEdit = nameof(CanTagEdit);
        public const string CanTagDeleteAjax = nameof(CanNewsDeleteAjax);
        public const string CanTagList = nameof(CanTagList);

        #endregion TagController

        #region NotificationController

        public const string CanRootNotification = nameof(CanRootNotification);
        public const string CanNotificationDeleteAjax = nameof(CanNotificationDeleteAjax);
        public const string CanNotificationMyList = nameof(CanNotificationMyList);

        #endregion NotificationController

        #region CompanyConversationController

        public const string CanRootCompanyConversation = nameof(CanRootCompanyConversation);
        public const string CanCompanyConversationDeleteAjax = nameof(CanCompanyConversationDeleteAjax);
        public const string CanCompanyConversationList = nameof(CanCompanyConversationList);
        public const string CanCompanyConversationCreate = nameof(CanCompanyConversationCreate);
        public const string CanCompanyConversationCreateAjax = nameof(CanCompanyConversationCreateAjax);
        public const string CanCompanyConversationMyList = nameof(CanCompanyConversationMyList);

        #endregion CompanyConversationController

        #region HomeController

        public const string CanRootHome = nameof(CanRootHome);
        public const string CanHomeBoardPage = nameof(CanHomeBoardPage);
        public const string CanHomeDashboardPage = nameof(CanHomeDashboardPage);

        #endregion HomeController

        #region ReceiptPaymentController

        public const string CanRootReceiptPayment = nameof(CanRootReceiptPayment);
        public const string CanReceiptPaymentPay = nameof(CanReceiptPaymentPay);
        public const string CanReceiptPaymentList = nameof(CanReceiptPaymentList);
        public const string CanReceiptPaymentMyList = nameof(CanReceiptPaymentMyList);

        #endregion ReceiptPaymentController

        #region PlanDiscountController

        public const string CanRootPlanDiscount = nameof(CanRootPlanDiscount);
        public const string CanPlanDiscountEdit = nameof(CanPlanDiscountEdit);
        public const string CanPlanDiscountCreate = nameof(CanPlanDiscountCreate);
        public const string CanPlanDiscountDeleteAjax = nameof(CanPlanDiscountDeleteAjax);
        public const string CanPlanDiscountList = nameof(CanPlanDiscountList);

        #endregion PlanDiscountController

        #region PlanController

        public const string CanRootPlan = nameof(CanRootPlan);
        public const string CanPlanEdit = nameof(CanPlanEdit);
        public const string CanPlanCreate = nameof(CanPlanCreate);
        public const string CanPlanDeleteAjax = nameof(CanPlanDeleteAjax);
        public const string CanPlanList = nameof(CanPlanList);

        #endregion PlanController

        #region ProductCommentController

        public const string CanRootProductComment = nameof(CanRootProductComment);
        public const string CanProductCommentDeleteAjax = nameof(CanProductCommentDeleteAjax);
        public const string CanProductCommentList = nameof(CanProductCommentList);
        public const string CanProductCommentEditApprove = nameof(CanProductCommentEditApprove);
        public const string CanProductCommentEditReject = nameof(CanProductCommentEditReject);
        public const string CanProductCommentCreate = nameof(CanProductCommentCreate);
        public const string CanProductCommentCreateAjax = nameof(CanProductCommentCreateAjax);
        public const string CanProductCommentMyEdit = nameof(CanProductCommentMyEdit);
        public const string CanProductCommentMyDeleteAjax = nameof(CanProductCommentMyDeleteAjax);
        public const string CanProductCommentMyList = nameof(CanProductCommentList);

        #endregion ProductCommentController

        #region ProductController

        public const string CanRootProduct = nameof(CanRootProduct);
        public const string CanProductEdit = nameof(CanProductEdit);
        public const string CanProductCreate = nameof(CanProductCreate);
        public const string CanProductDeleteAjax = nameof(CanProductDeleteAjax);
        public const string CanProductList = nameof(CanProductList);
        public const string CanProductEditReject = nameof(CanProductEditReject);
        public const string CanProductEditApprove = nameof(CanProductEditApprove);
        public const string CanProductMyEdit = nameof(CanProductMyEdit);
        public const string CanProductMyDeleteAjax = nameof(CanProductMyDeleteAjax);
        public const string CanProductMyList = nameof(CanProductMyList);

        #endregion ProductController

        #region ProductFeatureController

        public const string CanRootProductFeature = nameof(CanRootProductFeature);
        public const string CanProductFeatureDeleteAjax = nameof(CanProductFeatureDeleteAjax);
        public const string CanProductFeatureGetListAjax = nameof(CanProductFeatureGetListAjax);

        #endregion ProductFeatureController

        #region ProductImageController

        public const string CanRootProductImage = nameof(CanRootProductImage);
        public const string CanProductImageDeleteAjax = nameof(CanProductImageDeleteAjax);
        public const string CanProductImageGetListAjax = nameof(CanProductImageGetListAjax);

        #endregion ProductImageController

        #region ProductLikeController

        public const string CanRootProductLike = nameof(CanRootProductLike);
        public const string CanProductLikeMyToggleAjax = nameof(CanProductLikeMyToggleAjax);
        public const string CanProductLikeMyLikeList = nameof(CanProductLikeMyLikeList);
        public const string CanProductLikeMyLikerList = nameof(CanProductLikeMyLikerList);

        #endregion ProductLikeController

        #region ProductNotifyController

        public const string CanRootProductNotify = nameof(CanRootProductNotify);
        public const string CanProductNotifyMyToggleAjax = nameof(CanProductNotifyMyToggleAjax);

        #endregion ProductNotifyController

        #region ProductReviewController

        public const string CanRootProductReview = nameof(CanRootProductReview);
        public const string CanProductReviewCreate = nameof(CanProductReviewCreate);
        public const string CanProductReviewEdit = nameof(CanProductReviewEdit);
        public const string CanProductReviewDeleteAjax = nameof(CanProductReviewDeleteAjax);
        public const string CanProductReviewList = nameof(CanProductReviewList);

        #endregion ProductReviewController

        #region ProductSpecificationController

        public const string CanRootProductSpecification = nameof(CanRootProductSpecification);
        public const string CanProductSpecificationDeleteAjax = nameof(CanProductSpecificationDeleteAjax);
        public const string CanProductSpecificationEditAjax = nameof(CanProductSpecificationEditAjax);
        public const string CanProductSpecificationCreateAjax = nameof(CanProductSpecificationCreateAjax);

        #endregion ProductSpecificationController

        #region UserViolationController

        public const string CanRootUserViolation = nameof(CanRootUserViolation);
        public const string CanUserViolationCreate = nameof(CanUserViolationCreate);
        public const string CanUserViolationEdit = nameof(CanUserViolationEdit);
        public const string CanUserViolationDeleteAjax = nameof(CanUserViolationDeleteAjax);
        public const string CanUserViolationList = nameof(CanUserViolationList);

        #endregion UserViolationController

        #region RoleController

        public const string CanRootRole = nameof(CanRootRole);
        public const string CanRoleCreate = nameof(CanRoleCreate);
        public const string CanRoleEdit = nameof(CanRoleEdit);
        public const string CanRoleDeleteAjax = nameof(CanRoleDeleteAjax);
        public const string CanRoleList = nameof(CanRoleList);
        public const string CanPermissionGetListAjax = nameof(CanPermissionGetListAjax);

        #endregion RoleController

        #region SeoUrlController

        public const string CanRootSeoUrl = nameof(CanRootSeoUrl);
        public const string CanSeoUrlEdit = nameof(CanSeoUrlEdit);
        public const string CanSeoUrlCreate = nameof(CanSeoUrlCreate);
        public const string CanSeoUrlDeleteAjax = nameof(CanSeoUrlDeleteAjax);
        public const string CanSeoUrlList = nameof(CanSeoUrlList);

        #endregion SeoUrlController

        #region SpecificationController

        public const string CanRootSpecification = nameof(CanRootSpecification);
        public const string CanSpecificationCreate = nameof(CanSpecificationCreate);
        public const string CanSpecificationEdit = nameof(CanSpecificationEdit);
        public const string CanSpecificationDeleteAjax = nameof(CanSpecificationDeleteAjax);
        public const string CanSpecificationList = nameof(CanSpecificationList);
        public const string CanSpecificationGetListByCategoryAjax = nameof(CanSpecificationGetListByCategoryAjax);

        #endregion SpecificationController

        #region SpecificationOptionController

        public const string CanRootSpecificationOption = nameof(CanRootSpecificationOption);
        public const string CanSpecificationOptionCreate = nameof(CanSpecificationOptionCreate);
        public const string CanSpecificationOptionEdit = nameof(CanSpecificationOptionEdit);
        public const string CanSpecificationOptionDeleteAjax = nameof(CanSpecificationOptionDeleteAjax);
        public const string CanSpecificationOptionList = nameof(CanSpecificationOptionList);

        #endregion SpecificationOptionController

        #region UserSettingController

        public const string CanRootUserSetting = nameof(CanRootUserSetting);
        public const string CanUserSettingEdit = nameof(CanUserSettingEdit);
        public const string CanUserSettingMyEdit = nameof(CanUserSettingMyEdit);
        public const string CanUserSettingList = nameof(CanUserSettingList);

        #endregion UserSettingController

        #region CanReportController

        public const string CanReportCreate = nameof(CanReportCreate);

        #endregion

        #endregion Public Fields
    }
}