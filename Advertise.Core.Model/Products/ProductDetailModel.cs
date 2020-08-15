using System;
using System.Collections.Generic;
using Advertise.Core.Model.Catalogs;
using Advertise.Core.Model.Categories;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Products
{
    public class ProductDetailModel : BaseModel
    {
        public string BrandName { get; set; }
        public string CategoryAlias { get; set; }
        public Guid CategoryId { get; set; }
        public CategoryOptionModel CategoryOption { get; set; }
        public string CategoryTitle { get; set; }
        public string CompanyAlias { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyDescription { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyLogoFileName { get; set; }
        public string CompanySlogan { get; set; }
        public string CompanyTitle { get; set; }
        public DateTime? CreatedOn { get; set; }
        public double CurrentUserRate { get; set; }
        public string Description { get; set; }
        public decimal? DiscountPercent { get; set; }
        public IEnumerable<ProductFeatureModel> Features { get; set; }
        public bool HaveDiscount { get; set; }
        public Guid Id { get; set; }
        public int ImageCount { get; set; }
        public string ImageFileName { get; set; }
        public IEnumerable<ProductImageModel> Images { get; set; }
        public bool InitLike { get; set; }
        public bool InitNotify { get; set; }
        public bool IsExist { get; set; }
        public int LikeCount { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public decimal? PreviousPrice { get; set; }
        public decimal? Price { get; set; }
        public ProductCommentListModel ProductCommentList { get; set; }
        public string ProductFeatures { get; set; }
        public IEnumerable<ProductSpecificationModel> ProductSpecifications { get; set; }
        public decimal RateNumber { get; set; }
        public int RateUsers { get; set; }
        public SellType Sell { get; set; }
        public int TagCount { get; set; }
        public IEnumerable<ProductTagModel> Tags { get; set; }
        public string TagTitle { get; set; }
        public string Title { get; set; }
        public int VisitCount { get; set; }
        public Guid? ManufacturerId { get; set; }
        public bool? IsCatalog { get; set; }
        public Guid? CatalogId { get; set; }
        public IList<CatalogDetailModel> CatalogProducts { get; set; }
        public string GuaranteeTitle { get; set; }
        public ColorType Color { get; set; }
        public decimal? HighestPrice { get; set; }
        public decimal? LowestPrice { get; set; }
        public int CatalogCompanyCount { get; set; }
    }
}