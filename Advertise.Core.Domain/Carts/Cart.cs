using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Products;
using Advertise.Core.Domain.Users;

namespace Advertise.Core.Domain.Carts
{
    public class Cart : BaseEntity
    {
        public virtual int? ProductCount { get; set; }
        public virtual bool? IsOrder { get; set; }
        public virtual bool? IsCancel { get; set; }
        public virtual Product Product { get; set; }
        public virtual Guid? ProductId { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
    }
}
