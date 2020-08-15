using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Products
{
    public class ProductRateCreateModel : BaseModel
    {
        public Guid? ProductId { get; set; }
        public RateType? Rate { get; set; }
    }
}