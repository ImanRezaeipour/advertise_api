using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Specifications;

namespace Advertise.Core.Domain.Catalogs
{
    public class CatalogSpecification : BaseEntity
    {
        public virtual string Value { get; set; }
        public virtual Catalog Catalog { get; set; }
        public virtual Guid? CatalogId { get; set; }
        public virtual Specification Specification { get; set; }
        public virtual Guid? SpecificationId { get; set; }
        public virtual SpecificationOption SpecificationOption { get; set; }
        public virtual Guid? SpecificationOptionId { get; set; }
    }
}