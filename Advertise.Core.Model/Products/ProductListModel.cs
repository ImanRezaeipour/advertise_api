using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Categories;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Products
{
    public class ProductListModel : BaseModel
    {
        public IEnumerable<CategoryModel> Categories { get; set; }
        public IEnumerable<SelectList> CategoryList { get; set; }
        public IEnumerable<SelectList> CityList { get; set; }
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public int ProductAll { get; set; }
        public int ProductApproved { get; set; }
        public int ProductPendeing { get; set; }
        public int ProductReject { get; set; }
        public IEnumerable<ProductModel> Products { get; set; }
        public ProductSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
        public IEnumerable<SelectList> StateTypeList { get; set; }
        public IEnumerable<SelectList> UserList { get; set; }
    }
}