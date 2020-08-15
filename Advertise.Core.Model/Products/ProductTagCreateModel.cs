using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Products
{
    public class ProductTagCreateModel : BaseModel
    {
        public DateTime? ExpiredOn { get; set; }
        public DateTime? StartedOn { get; set; }
        public Guid TagId { get; set; }
    }
}