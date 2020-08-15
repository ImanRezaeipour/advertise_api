using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Specifications;

namespace Advertise.Core.Domain.Products
{
    public class ProductSpecification : BaseEntity
    {
        public virtual string Value { get; set; }
        public virtual Product Product { get; set; }
        public virtual Guid? ProductId { get; set; }
        public virtual Specification Specification { get; set; }
        public virtual Guid? SpecificationId { get; set; }
        public virtual SpecificationOption SpecificationOption { get; set; }
        public virtual Guid? SpecificationOptionId { get; set; }
    }
}