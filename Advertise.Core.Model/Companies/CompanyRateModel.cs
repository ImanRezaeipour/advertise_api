using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Companies
{
    public class CompanyRateModel : BaseModel
    {
        public Guid? CompanyId { get; set; }
        public bool? IsApprove { get; set; }
        public RateType? Rate { get; set; }
    }
}