using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Products
{
    public class ProductCommentLikeSearchModel : BaseSearchModel
    {
        public Guid? CommentId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? LikedById { get; set; }
        public StateType? State { get; set; }
    }
}