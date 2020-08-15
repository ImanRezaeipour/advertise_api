using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Products
{
    public class ProductBulkModel : BaseModel
    {
        public int? AvailableCount { get; set; }
        public Guid? CatalogId { get; set; }
        public Guid? CategoryId { get; set; }
        public ColorType? Color { get; set; }
        public string Description { get; set; }
        public string GuaranteeId { get; set; }
        public Guid? Id { get; set; }
        public bool? IsSecondHand { get; set; }
        public decimal? Price { get; set; }
        public string SecondHandCode { get; set; }
        public string GuaranteeTitle { get; set; }
    }
}