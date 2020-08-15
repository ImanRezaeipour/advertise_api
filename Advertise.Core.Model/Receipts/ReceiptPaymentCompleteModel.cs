using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Receipts
{
    public class ReceiptPaymentCompleteModel : BaseModel
    {
        public string Color { get; set; }
        public string InvoiceNumber { get; set; }
        public string Message { get; set; }
    }
}