using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Users;

namespace Advertise.Core.Domain.Products
{
    public class ProductNotify : BaseEntity
    {
        public virtual Product Product { get; set; }
        public virtual Guid? ProductId { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
    }
}