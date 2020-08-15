using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Users
{
    public class UserViolationModel : BaseModel
    {
        public DateTime CreatedOn { get; set; }
        public Guid Id { get; set; }
        public bool IsRead { get; set; }
        public string Reason { get; set; }
        public string ReasonDescription { get; set; }
        public string TargetFullName { get; set; }
        public string TargetUserName { get; set; }
        public ReasonType Type { get; set; }
        public string UserAvatar { get; set; }
        public string UserFullName { get; set; }
        public string UserUserName { get; set; }
    }
}