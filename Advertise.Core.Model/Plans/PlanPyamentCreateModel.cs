using System.Collections.Generic;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Plans
{
    public class PlanPyamentCreateModel : BaseModel
    {
        public decimal? Amount { get; set; }
        public string Code { get; set; }
        public string DiscountCode { get; set; }
        public PayType Pay { get; set; }
        public IEnumerable<PlanModel> Plans { get; set; }
    }
}