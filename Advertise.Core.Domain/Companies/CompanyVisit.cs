using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Users;

namespace Advertise.Core.Domain.Companies
{
    public class CompanyVisit : BaseVisit
    {
        public virtual User VisitedBy { get; set; }
        public virtual Guid? VisitedById { get; set; }
        public virtual Company Company { get; set; }
        public virtual Guid? CompanyId { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
    }
}