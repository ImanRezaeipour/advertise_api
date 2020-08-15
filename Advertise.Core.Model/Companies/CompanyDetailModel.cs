using System;
using System.Collections.Generic;
using Advertise.Core.Model.Categories;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Locations;
using Advertise.Core.Model.Products;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Companies
{
    public class CompanyDetailModel : BaseModel
    {
        public LocationModel Location { get; set; }
        public string Alias { get; set; }
        public CompanyAttachmentListModel AttachmentList { get; set; }
        public string CategoryAlias { get; set; }
        public Guid CategoryId { get; set; }
        public CategoryOptionModel CategoryOption { get; set; }
        public string CategoryTitle { get; set; }
        public string Code { get; set; }
        public IEnumerable<CompanyConversationModel> ConversationList { get; set; }
        public string CoverFileName { get; set; }
        public Guid CreatedById { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public EmployeeRangeType? EmployeeRange { get; set; }
        public DateTime? EstablishedOn { get; set; }
        public string FacebookLink { get; set; }
        public int FollowerCount { get; set; }
        public string FullName { get; set; }
        public string GooglePlusLink { get; set; }
        public Guid Id { get; set; }
        public int ImageCount { get; set; }
        public string CategoryOptionProducts { get; set; }
        public string CategoryOptionCompanies { get; set; }
        public bool InitFollow { get; set; }
        public string InstagramLink { get; set; }
        public bool IsMyself { get; set; }
        public IEnumerable<CompanyImageModel> ImageList { get; set; }
        public string LogoFileName { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public string MobileNumber { get; set; }
        public string PhoneNumber { get; set; }
        public int ProductCount { get; set; }
        public ProductListModel ProductList { get; set; }
        public CompanyQuestionListModel QuestionList { get; set; }
        public CompanyReviewListModel ReviewList { get; set; }
        public string Slogan { get; set; }
        public int TagCount { get; set; }
        public string TelegramLink { get; set; }
        public string Title { get; set; }
        public string TwitterLink { get; set; }
        public string UserCode { get; set; }
        public string UserEmail { get; set; }
        public string UserUserName { get; set; }
        public IEnumerable<CompanyVideoModel> VideoList { get; set; }
        public int VisitCount { get; set; }
        public string WebSite { get; set; }
        public string YoutubeLink { get; set; }
    }
}