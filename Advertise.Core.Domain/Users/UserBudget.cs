using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Receipts;

namespace Advertise.Core.Domain.Users
{
    public class UserBudget : BaseEntity
    {
        public virtual int? RemainValue { get; set; }
        public virtual int? IncDecValue { get; set; }
        public virtual string Description { get; set; }
        public virtual ReceiptPayment Payment { get; set; }
        public virtual Guid? PaymentId { get; set; }
    }
}