using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Products
{
    public class ProductLikeSearchModel : BaseSearchModel
    {
        public Guid? CompanyId { get; set; }
        public bool? IsLike { get; set; }
        public Guid? LikedById { get; set; }
        public Guid? ProductId { get; set; }
    }
}