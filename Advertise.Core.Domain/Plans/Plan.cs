using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Roles;
using Advertise.Core.Domain.Users;
using Advertise.Core.Types;

namespace Advertise.Core.Domain.Plans
{
    public class Plan : BaseEntity
    {
        public virtual string Code { get; set; }
        public virtual int? DurationDay { get; set; }
        public virtual decimal? Price { get; set; }
        public virtual Role Role { get; set; }
        public virtual Guid? RoleId { get; set; }
        public virtual string Title { get; set; }
        public virtual decimal? PreviousePrice { get; set; }
        public virtual ColorType? Color { get; set; }
        public virtual bool? IsActive { get; set; }
        public virtual PlanDurationType? PlanDuration { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual DateTime? ModifiedOn { get; set; }
        public virtual Guid? CreatedById { get; set; }
    }
}