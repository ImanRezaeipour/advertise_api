using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Users
{
    public class UserHeaderModel : BaseModel
    {
        public string AvatarFileName { get; set; }
        public int BagCount { get; set; }
        public string CompanyAlias { get; set; }
        public string CompanyTitle { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public bool IsSetSubdomain { get; set; }
        public bool IsSetUserName { get; set; }
        public string UserName { get; set; }
    }
}