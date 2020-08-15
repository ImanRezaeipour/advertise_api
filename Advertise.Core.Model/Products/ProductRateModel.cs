using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Products
{
    public class ProductRateModel : BaseModel
    {
        public bool? IsApprove { get; set; }
        public Guid? ProductId { get; set; }
        public RateType? Rate { get; set; }
    }
}