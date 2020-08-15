using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Users;

namespace Advertise.Core.Domain.Products
{
    public class ProductImage : BaseImage
    {
        public virtual int? Order { get; set; }
        public virtual bool? IsWatermark { get; set; }
        public virtual Product Product { get; set; }
        public virtual Guid? ProductId { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
    }
}