using System;
using System.Collections.Generic;
using Advertise.Core.Domain.Categories;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Products;
using Advertise.Core.Types;

namespace Advertise.Core.Domain.Specifications
{
    public class Specification : BaseEntity
    {
        public virtual string Title { get; set; }
        public virtual SpecificationType? Type { get; set; }
        public virtual string Description { get; set; }
        public virtual int? Order { get; set; }
        public virtual Category Category { get; set; }
        public virtual Guid? CategoryId { get; set; }
        public virtual bool? IsSearchable { get; set; }
        public virtual ICollection<SpecificationOption> Options { get; set; }
        public virtual ICollection<ProductSpecification> Products { get; set; }
    }
}