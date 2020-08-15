using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Products
{
    public class ProductCommentLikeListModel : BaseModel
    {
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public IEnumerable<ProductCommentLikeModel> ProductComments { get; set; }
        public ProductCommentLikeSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
    }
}