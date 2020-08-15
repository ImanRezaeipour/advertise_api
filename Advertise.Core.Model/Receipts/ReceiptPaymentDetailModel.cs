using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Receipts
{
    public class ReceiptPaymentDetailModel : BaseModel
    {
        public Guid Id { get; set; }
        public bool IsSuccess { get; set; }
        public string ReferenceCode { get; set; }
        public long Value { get; set; }
    }
}