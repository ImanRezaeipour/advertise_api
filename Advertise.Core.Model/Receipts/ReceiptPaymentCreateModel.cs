using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Receipts
{
    public class ReceiptPaymentCreateModel : BaseModel
    {
        public bool IsSuccess { get; set; }
        public string ReferenceCode { get; set; }
        public long Value { get; set; }
    }
}