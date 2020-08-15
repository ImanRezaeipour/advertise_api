using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Users;

namespace Advertise.Core.Domain.Receipts
{
    public class ReceiptOption : BaseEntity
    {
        public virtual string Title { get; set; }
        public virtual string Code { get; set; }
        public virtual decimal? Price { get; set; }
        public virtual decimal? Count { get; set; }
        public virtual decimal? TotalPrice { get; set; }
        public virtual decimal? PreviousPrice { get; set; }
        public virtual decimal? DiscountPercent { get; set; }
        public virtual string CompanyTitle { get; set; }
        public virtual string CompanyCode { get; set; }
        public virtual string CategoryTitle { get; set; }
        public virtual string CategoryCode { get; set; }
        public virtual Guid? SaledById { get; set; }
        public virtual Receipt Receipt { get; set; }
        public virtual Guid? ReceiptId { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
    }
}