using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Users;

namespace Advertise.Core.Domain.Plans
{
    public class PlanPayment : BaseEntity
    {
        public decimal? Amount { get; set; }
        public string AuthorityCode { get; set; }
        public string Description { get; set; }
        public bool? IsComplete { get; set; }
        public bool? IsSuccess { get; set; }
        public string MerchantCode { get; set; }
        public long? ReferenceCode { get; set; }
        public int? StatusCode { get; set; }
        public virtual Plan Plan { get; set; }
        public virtual Guid? PlanId { get; set; }
        public virtual PlanDiscount PlanDiscount { get; set; }
        public virtual Guid? PlanDiscountId { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
    }
}