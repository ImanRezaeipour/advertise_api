using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Domain.Users
{
    public class UserViolation : BaseEntity
    {
        public virtual ReportType? Type { get; set; }
        public virtual ReasonType? Reason { get; set; }
        public virtual string ReasonDescription { get; set; }
        public virtual bool? IsRead { get; set; }
        public virtual Guid? TargetId { get; set; }
        public virtual User ReportedBy { get; set; }
        public virtual Guid? ReportedById { get; set; }
    }
}