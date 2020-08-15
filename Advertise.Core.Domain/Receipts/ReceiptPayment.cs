using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Users;
using Advertise.Core.Types;

namespace Advertise.Core.Domain.Receipts
{
    public class ReceiptPayment : BaseEntity
    {
        public string MerchantCode { get; set; }
        public string AuthorityCode { get; set; }
        public long? ReferenceCode { get; set; }
        public int? StatusCode { get; set; }
        public PayType Pay { get; set; }
        public BuyType Buy { get; set; }
        public bool? IsComplete { get; set; }
        public bool? IsSuccess { get; set; }
        public int? Amount { get; set; }
        public string Description { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public virtual User PayedBy { get; set; }
        public virtual Guid? PayedById { get; set; }
        public virtual Receipt Receipt { get; set; }
        public virtual Guid? ReceiptId { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
    }
}