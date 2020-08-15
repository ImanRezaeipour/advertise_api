using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Products
{
    public class ProductLikeCreateModel : BaseModel
    {
        public bool IsLike { get; set; }
        public Guid LikedById { get; set; }
        public Guid ProductId { get; set; }
    }
}