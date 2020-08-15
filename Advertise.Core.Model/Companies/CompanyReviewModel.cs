using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Companies
{
    public class CompanyReviewModel : BaseModel
    {
        public bool Active { get; set; }
        public string AvatarFileName { get; set; }
        public string Body { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public string CompanyAlias { get; set; }
        public string CompanyLogo { get; set; }
        public string CompanyLogoFileName { get; set; }
        public string CompanyTitle { get; set; }
        public DateTime CreatedOn { get; set; }
        public string FullName { get; set; }
        public Guid Id { get; set; }
        public string RejectDescription { get; set; }
        public StateType State { get; set; }
        public string Title { get; set; }
        public Guid UserId { get; set; }
    }
}