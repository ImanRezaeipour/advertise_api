using System.Collections.Generic;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Products;
using Advertise.Core.Types;

namespace Advertise.Core.Domain.Tags
{
    public class Tag : BaseEntity
    {
        public virtual string Code { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual string LogoFileName { get; set; }
        public virtual string CostValue { get; set; }
        public virtual string DurationDay { get; set; }
        public virtual ColorType Color { get; set; }
        public virtual bool? IsActive { get; set; }
        public virtual ICollection<ProductTag> Products { get; set; }
    }
}