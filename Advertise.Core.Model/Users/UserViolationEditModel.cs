using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Users
{
    public class UserViolationEditModel : BaseModel
    {
        public Guid Id { get; set; }
        public bool IsRead { get; set; }
        public string Reason { get; set; }
        public ReasonType Type { get; set; }
    }
}