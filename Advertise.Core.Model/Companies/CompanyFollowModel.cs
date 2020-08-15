using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyFollowModel : BaseModel
    {
        public string AvatarFileName { get; set; }
        public CompanyDetailModel Company { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? FollowedById { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
    }
}