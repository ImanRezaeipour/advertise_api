using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Products
{
    public class ProductVisitListModel : BaseModel
    {
        public IEnumerable<SelectList> ActiveList { get; set; }
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public IEnumerable<ProductVisitModel> ProductVisits { get; set; }
        public ProductVisitSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
        public IEnumerable<SelectList> StateList { get; set; }
        public IEnumerable<SelectList> UserList { get; set; }
    }
}