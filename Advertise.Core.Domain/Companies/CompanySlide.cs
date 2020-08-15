using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Products;

namespace Advertise.Core.Domain.Companies
{
    public class CompanySlide : BaseEntity
    {
        public virtual string Title { get; set; }
        public virtual string ImageFileName { get; set; }
        public virtual string Description { get; set; }
        public virtual int Order { get; set; }
        public virtual Company Company { get; set; }
        public virtual Guid? CompanyId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Guid? ProductId { get; set; }
    }
}