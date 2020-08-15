using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Products
{
    public class ProductCommentSearchModel : BaseSearchModel
    {
        public Guid? CommentedById { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? ProductId { get; set; }
        public StateType? State { get; set; }
    }
}