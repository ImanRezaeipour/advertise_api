using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Products;
using Advertise.Core.Types;

namespace Advertise.Core.Domain.Users
{
    public class UserNotification : BaseEntity
    {
        public virtual bool? IsRead { get; set; }
        public virtual string Url { get; set; }
        public virtual string Message { get; set; }
        public virtual DateTime? ReadOn { get; set; }
        public virtual NotificationType Type { get; set; }
        public virtual Guid? TargetId { get; set; }
        public virtual Product Target { get; set; }
        public virtual Guid? OwnedById { get; set; }
        public virtual string TargetTitle { get; set; }
        public virtual string TargetImage { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
    }
}