using System;
using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Categories;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Specifications;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Products
{
    public class ProductSearchModel : BaseSearchModel
    {
        public string CategoryAlias { get; set; }
        public string SecondHandCode { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? CatalogId { get; set; }
        public IEnumerable<int> Colors { get; set; }
        public bool? CategoryWithMany { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? CreatedById { get; set; }
        public IEnumerable<Guid> Ids { get; set; }
        public StateType? State { get; set; }
        public Guid? StateId { get; set; }
        public Guid? UserId { get; set; }
        public string Code { get; set; }
        public SellType? Sell { get; set; }
        public int? AvailableCountGreater { get; set; }
        public bool? DistinctByCompanyId { get; set; }
        public bool? IsSecondHand { get; set; }
        public ColorType? ColorType { get; set; }
        public Guid? GuaranteeId { get; set; }
        public string QueryString { get; set; }
        public string Price { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public Dictionary<string, List<string>> SpecificationsDictionary { get; set; }
        public IEnumerable<CategoryModel> Categories { get; set; }
        public IEnumerable<SpecificationModel> Specifications { get; set; }
        public IEnumerable<CategoryModel> CategoryList { get; set; }
        public IEnumerable<SelectList> CityList { get; set; }
        public IEnumerable<SelectList> PageSizeFilterList { get; set; }
        public IEnumerable<ProductModel> Products { get; set; }
        public ColorType Color { get; set; }
        public ProductSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionFilterList { get; set; }
        public IEnumerable<SelectList> SortMemberFilterList { get; set; }
        public IEnumerable<SelectList> RequestValues { get; set; }
    }
}