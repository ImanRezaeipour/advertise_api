using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Users
{
    public class UserOnlineModel : BaseModel
    {
        public bool IsActive { get; set; }
        public string SessionId { get; set; }
    }
}