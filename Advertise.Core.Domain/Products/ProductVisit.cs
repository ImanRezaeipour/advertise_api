using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Users;

namespace Advertise.Core.Domain.Products
{
    public class ProductVisit : BaseVisit
    {
        public virtual bool? IpAddress { get; set; }
        public virtual User VisitedBy { get; set; }
        public virtual Guid? VisitedById { get; set; }
        public virtual Product Product { get; set; }
        public virtual Guid? ProductId { get; set; }
    }
}