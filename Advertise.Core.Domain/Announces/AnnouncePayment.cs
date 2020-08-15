using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Users;
using Advertise.Core.Types;

namespace Advertise.Core.Domain.Announces
{
    public class AnnouncePayment :BaseEntity
    {
        public virtual long? ReferenceCode { get; set; }
        public virtual decimal? Amount { get; set; }
        public virtual PaymentType? Type { get; set; }
        public virtual string AuthorityCode { get; set; }
        public virtual string Description { get; set; }
        public virtual bool? IsComplete { get; set; }
        public virtual bool? IsSuccess { get; set; }
        public virtual string MerchantCode { get; set; }
        public virtual int? StatusCode { get; set; }
        public virtual Announce Announce { get; set; }
        public virtual Guid? AnnounceId { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
    }
}