using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Categories
{
    public class CategoryListModel : BaseModel
    {
        public IEnumerable<SelectList> ActiveList { get; set; }
        public IEnumerable<CategoryModel> Categories { get; set; }
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public CategorySearchModel SearchRequest { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
    }
}