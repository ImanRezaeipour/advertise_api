using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Tags;
using Advertise.Core.Domain.Users;

namespace Advertise.Core.Domain.Companies
{
    public class CompanyTag : BaseEntity
    {
        public virtual DateTime? StartedOn { get; set; }
        public virtual DateTime? ExpiredOn { get; set; }
        public virtual Tag Tag { get; set; }
        public virtual Guid? TagId { get; set; }
        public virtual Company Company { get; set; }
        public virtual Guid? CompanyId { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
    }
}