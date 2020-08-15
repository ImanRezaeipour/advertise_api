using Advertise.Core.Model.Common;
using Advertise.Core.Model.Locations;

namespace Advertise.Core.Model.Users
{
    public class UserDetailInfoModel : BaseModel
    {
        public LocationModel Location { get; set; }
        public string Email { get; set; }
        public string HomeNumber { get; set; }
        public string PhoneNumber { get; set; }
    }
}