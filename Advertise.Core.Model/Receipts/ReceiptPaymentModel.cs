using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Receipts
{
    public class ReceiptPaymentModel : BaseModel
    {
        public int? Amount { get; set; }
        public BuyType Buy { get; set; }
        public string Email { get; set; }
        public Guid Id { get; set; }
        public bool IsSuccess { get; set; }
        public string MobileNumber { get; set; }
        public PayType Pay { get; set; }
        public string ReferenceCode { get; set; }
        public long Value { get; set; }
    }
}