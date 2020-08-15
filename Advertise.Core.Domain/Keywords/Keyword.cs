using System;
using System.Collections.Generic;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Products;
using Advertise.Core.Domain.Users;

namespace Advertise.Core.Domain.Keywords
{
    public class Keyword : BaseEntity
    {
        public virtual string Title { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual ICollection<ProductKeyword> ProductKeywords { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
    }
}
