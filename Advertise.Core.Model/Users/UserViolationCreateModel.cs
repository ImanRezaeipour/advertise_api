using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Users
{
    public class UserViolationCreateModel : BaseModel
    {
        public bool IsRead { get; set; }
        public string Reason { get; set; }
        public ReasonType Type { get; set; }
    }
}