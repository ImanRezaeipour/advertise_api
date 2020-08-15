using System;
using System.Collections.Generic;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Locations;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Products
{
    public class ProductModel : BaseModel
    {
        public LocationModel Location { get; set; }
        public string CatAlias { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public string CatMetaTitle { get; set; }
        public string Code { get; set; }
        public string CompanyAlias { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyLogoFileName { get; set; }
        public string CompanyTitle { get; set; }
        public Guid CreatedById { get; set; }
        public string Description { get; set; }
        public decimal? DiscountPercent { get; set; }
        public string Email { get; set; }
        public Guid Id { get; set; }
        public string ImageFileName { get; set; }
        public IEnumerable<ProductImageModel> Images { get; set; }
        public bool IsExist { get; set; }
        public string MobileNumber { get; set; }
        public string PhoneNumber { get; set; }
        public decimal? PreviousPrice { get; set; }
        public decimal? Price { get; set; }
        public string RejectDescription { get; set; }
        public SellType Sell { get; set; }
        public StateType State { get; set; }
        public int TagCount { get; set; }
        public IEnumerable<ProductTagModel> Tags { get; set; }
        public string TagTitle { get; set; }
        public string Title { get; set; }
        public bool? IsCatalog { get; set; }
        public Guid? CatalogId { get; set; }
        public Guid? ManufacturerId { get; set; }
        public decimal? HighestPrice { get; set; }
        public decimal? LowestPrice { get; set; }
        public int CatalogCompanyCount { get; set; }
        public string GuaranteeTitle { get; set; }
    }
}