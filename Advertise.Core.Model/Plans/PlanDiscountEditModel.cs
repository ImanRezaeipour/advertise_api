using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Plans
{
    public class PlanDiscountEditModel : BaseModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public int? Count { get; set; }
        public DateTime? ExpiredOn { get; set; }
        public int? Max { get; set; }
        public int? Percent { get; set; }
    }
}