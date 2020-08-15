using System;
using Advertise.Core.Domain.Common;

namespace Advertise.Core.Domain.Users
{
    public class UserOnline : BaseEntity
    {
        public virtual string SessionId { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
    }
}