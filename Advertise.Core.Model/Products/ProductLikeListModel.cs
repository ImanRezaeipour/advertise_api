using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Products
{
    public class ProductLikeListModel : BaseModel
    {
        public IEnumerable<SelectList> LikeList { get; set; }
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public IEnumerable<ProductLikeModel> ProductLikes { get; set; }
        public IEnumerable<SelectList> ProductList { get; set; }
        public ProductLikeSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
        public IEnumerable<SelectList> UserList { get; set; }
    }
}