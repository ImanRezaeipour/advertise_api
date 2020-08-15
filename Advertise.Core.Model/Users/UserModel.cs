using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Users
{
    public class UserModel : BaseModel
    {
        public string AvatarFileName { get; set; }
        public string CompanyAlias { get; set; }
        public string CompanyLogoFileName { get; set; }
        public string CompanyTitle { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public Guid Id { get; set; }
        public bool? IsBan { get; set; }
        public bool? IsSetUserName { get; set; }
        public bool? IsSystemAccount { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
    }
}