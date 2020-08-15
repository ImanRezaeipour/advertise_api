using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Plans
{
    public class PlanDiscountCreateModel : BaseModel
    {
        public string Code { get; set; }
        public int? Count { get; set; }
        public DateTime? Expire { get; set; }
        public int? Max { get; set; }
        public int? Percent { get; set; }
    }
}