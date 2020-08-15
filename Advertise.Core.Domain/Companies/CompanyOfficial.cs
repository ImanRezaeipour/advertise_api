using System;
using Advertise.Core.Domain.Common;

namespace Advertise.Core.Domain.Companies
{
    public class CompanyOfficial : BaseEntity
    {
        public virtual string BusinessLicenseFileName { get; set; }
        public virtual bool? IsApprove { get; set; }
        public virtual bool IsComplete { get; set; }
        public virtual string NationalCardFileName { get; set; }
        public virtual string OfficialNewspaperAddress { get; set; }
        public virtual Company Company { get; set; }
        public virtual Guid? CompanyId { get; set; }
    }
}