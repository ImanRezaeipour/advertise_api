using System;
using Advertise.Core.Domain.Common;

namespace Advertise.Core.Domain.Products
{
    public class ProductFeature : BaseEntity
    {
        public string Title { get; set; }
        public virtual Product Product { get; set; }
        public virtual Guid? ProductId { get; set; }
    }
}