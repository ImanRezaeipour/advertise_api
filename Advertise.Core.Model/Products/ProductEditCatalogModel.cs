using System;
using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Products
{
    public class ProductEditCatalogModel : BaseModel
    {
        public int? AvailableCount { get; set; }
        public Guid? CatalogId { get; set; }
        public string CatalogListJson { get; set; }
        public Guid? CategoryId { get; set; }
        public string CategoryListJson { get; set; }
        public ColorType? Color { get; set; }
        public IEnumerable<SelectList> ColorList { get; set; }
        public Guid? GuaranteeId { get; set; }
        public IEnumerable<SelectList> GuaranteeList { get; set; }
        public Guid? Id { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public bool? IsSecondHand { get; set; }
        public string SecondHandCode { get; set; }
        public string GuaranteeTitle { get; set; }
    }
}