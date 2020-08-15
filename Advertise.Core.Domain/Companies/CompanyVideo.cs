using System;
using System.Collections.Generic;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Users;
using Advertise.Core.Types;

namespace Advertise.Core.Domain.Companies
{
    public class CompanyVideo : BaseEntity
    {
        public virtual string Title { get; set; }
        public virtual int? Order { get; set; }
        public virtual StateType? State { get; set; }
        public virtual string RejectDescription { get; set; }
        public virtual Company Company { get; set; }
        public virtual Guid? CompanyId { get; set; }
        public virtual User User { get; set; }
        public virtual Guid? ApprovedById { get; set; }
        public virtual ICollection<CompanyVideoFile> VideoFiles { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
    }
}
