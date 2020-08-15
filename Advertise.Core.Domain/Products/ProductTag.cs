using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Tags;

namespace Advertise.Core.Domain.Products
{
    public class ProductTag : BaseEntity
    {
        public virtual DateTime? StartedOn { get; set; }
        public virtual DateTime? ExpiredOn { get; set; }
        public virtual Tag Tag { get; set; }
        public virtual Guid? TagId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Guid? ProductId { get; set; }
    }
}