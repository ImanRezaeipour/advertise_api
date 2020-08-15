using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Products
{
    public class ProductImageListModel : BaseModel
    {
        public IEnumerable<SelectList> CompanyList { get; set; }
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public IEnumerable<ProductImageModel> ProductImages { get; set; }
        public ProductImageSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
        public IEnumerable<SelectList> UserList { get; set; }
    }
}