using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Plans
{
    public class PlanPaymentModel : BaseModel
    {
        public int? Amount { get; set; }
        public string AuthorityCode { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Description { get; set; }
        public Guid Id { get; set; }
        public bool? IsComplete { get; set; }
        public bool? IsSuccess { get; set; }
        public string MerchantCode { get; set; }
        public Guid? PlanId { get; set; }
        public string PlanTitle { get; set; }
        public long? ReferenceCode { get; set; }
        public int? StatusCode { get; set; }
    }
}