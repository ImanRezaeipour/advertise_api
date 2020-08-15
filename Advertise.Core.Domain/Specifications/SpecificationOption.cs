using System;
using Advertise.Core.Domain.Common;

namespace Advertise.Core.Domain.Specifications
{
    public class SpecificationOption : BaseEntity
    {
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual Specification Specification { get; set; }
        public virtual Guid? SpecificationId { get; set; }
    }
}