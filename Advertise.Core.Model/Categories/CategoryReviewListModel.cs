using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Categories
{
    public class CategoryReviewListModel : BaseModel
    {
        public IEnumerable<SelectList> ActiveList { get; set; }
        public IEnumerable<SelectList> CategoryList { get; set; }
        public IEnumerable<CategoryReviewModel> CategoryReviews { get; set; }
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public CategoryReviewSearchModel SearchRequest { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
        public IEnumerable<SelectList> UserList { get; set; }
    }
}