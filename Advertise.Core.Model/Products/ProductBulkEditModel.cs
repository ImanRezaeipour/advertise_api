using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Products
{
    public class ProductBulkEditModel : BaseModel
    {
        public string CatalogListJson { get; set; }
        public string CategoryListJson { get; set; }
        public IEnumerable<SelectList> ColorList { get; set; }
        public IEnumerable<SelectList> GuaranteeList { get; set; }
        public IEnumerable<ProductBulkModel> ProductBulks { get; set; }
    }
}