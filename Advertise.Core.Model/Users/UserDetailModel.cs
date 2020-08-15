using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Locations;

namespace Advertise.Core.Model.Users
{
    public class UserDetailModel : BaseModel
    {
        public LocationModel Location { get; set; }
        public string AvatarFileName { get; set; }
        public string CompanyAlias { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyTitle { get; set; }
        public Guid CreatedById { get; set; }
        public string Email { get; set; }
        public string Followers { get; set; }
        public string FullName { get; set; }
        public string HomeNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
    }
}