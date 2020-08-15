using System;
using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Tags;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Products
{
    public class ProductEditModel : BaseModel
    {
        public string Body { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public string Code { get; set; }
        public Guid CompanyId { get; set; }
        public Guid CreatedById { get; set; }
        public string Description { get; set; }
        public IEnumerable<ProductFeatureCreateModel> Features { get; set; }
        public Guid Id { get; set; }
        public string ImageFileName { get; set; }
        public IEnumerable<ProductImageCreateModel> Images { get; set; }
        public IEnumerable<SelectList> KeywordList { get; set; }
        public decimal PreviousPrice { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<Guid?> ProductKeywordList { get; set; }
        public IEnumerable<string> ProductKeywords { get; set; }
        public IEnumerable<ProductTagCreateModel> ProductTags { get; set; }
        public SellType Sell { get; set; }
        public IEnumerable<SelectList> SellTypeList { get; set; }
        public IEnumerable<ProductSpecificationEditModel> Specifications { get; set; }
        public IEnumerable<TagModel> Tags { get; set; }
        public string Title { get; set; }
    }
}