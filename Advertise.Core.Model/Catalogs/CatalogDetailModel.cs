using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Catalogs
{
    public class CatalogDetailModel : BaseModel
    {
        public Guid CompanyId { get; set; }
        public Guid ProductId { get; set; }
        public string CompanyAlias { get; set; }
        public string CompanyTitle { get; set; }
        public string CompanyLogoFileName { get; set; }
        public DateTime? ProductModifiedOn { get; set; }
        public Guid? ManufacturerId { get; set; }
        public SellType ProductSell { get; set; }
        public ColorType ProductColor { get; set; }
        public string ProductGuaranteeTitle { get; set; }
        public decimal? ProductPrice { get; set; }
        public bool ProductIsExist { get; set; }
    }
}