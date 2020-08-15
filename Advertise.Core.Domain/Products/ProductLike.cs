using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Users;

namespace Advertise.Core.Domain.Products
{
    public class ProductLike : BaseEntity
    {
        public virtual bool? IsLike { get; set; }
        public virtual User LikedBy { get; set; }
        public virtual Guid? LikedById { get; set; }
        public virtual Product Product { get; set; }
        public virtual Guid? ProductId { get; set; }
    }
}