using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Users;
using Advertise.Core.Types;

namespace Advertise.Core.Domain.Products
{
    public class ProductReview : BaseReview
    {
        public virtual StateType? State { get; set; }
        public virtual string RejectDescription { get; set; }
        public virtual Product Product { get; set; }
        public virtual Guid? ProductId { get; set; }
        public virtual User ApprovedBy { get; set; }
        public virtual Guid? ApprovedById { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
    }
}