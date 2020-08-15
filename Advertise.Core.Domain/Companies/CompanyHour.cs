using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Users;
using Advertise.Core.Types;

namespace Advertise.Core.Domain.Companies
{
    public class CompanyHour : BaseEntity
    {
        public virtual DayType? DayOfWeek { get; set; }
        public virtual TimeSpan? EndedOn { get; set; }
        public virtual TimeSpan? StartedOn { get; set; }
        public virtual bool? IsActive { get; set; }
        public virtual Company Company { get; set; }
        public virtual Guid? CompanyId { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
    }
}