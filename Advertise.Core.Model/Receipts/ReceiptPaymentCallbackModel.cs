using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Receipts
{
    public class ReceiptPaymentCallbackModel : BaseModel
    {
        public string Authority { get; set; }
        public string Status { get; set; }
    }
}