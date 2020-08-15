using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Announces
{
    public class AnnouncePaymentCreateModel : BaseModel
    {
        public decimal? Amount { get; set; }
        public string Callbak { get; set; }
        public Guid? AdsId { get; set; }
        public PayType Pay { get; set; }
        public string ReferenceCode { get; set; }
        public PaymentType? Type { get; set; }
    }
}