using System;
using System.Collections.Generic;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Users;

namespace Advertise.Core.Domain.Plans
{
    public class PlanDiscount : BaseEntity
    {
        public virtual string Code { get; set; }
        public virtual int? Percent { get; set; }
        public virtual int? Max { get; set; }
        public virtual int? Count { get; set; }
        public virtual DateTime? Expire { get; set; }
        public virtual ICollection <PlanPayment> PlanPeyments { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
    }
}
