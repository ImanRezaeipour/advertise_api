using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanySlideSearchModel : BaseSearchModel
    {
        public DateTime? CreatedOn { get; set; }
        public Guid? CreatedById { get; set; }
        public Guid? CompanyId { get; set; }
    }
}