using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Plans
{
    public class PlanPaymentCallbackModel : BaseModel
    {
        public string Authority { get; set; }
        public PayType Pay { get; set; }
        public string Status { get; set; }
    }
}