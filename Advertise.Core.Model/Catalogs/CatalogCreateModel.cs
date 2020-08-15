using System;
using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Catalogs
{
    public class CatalogCreateModel : BaseModel
    {
        public string Body { get; set; }
        public Guid CategoryId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Guid? ManufacturerId { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaTitle { get; set; }
        public string Title { get; set; }
        public IEnumerable<CatalogSpecificationModel> Specifications { get; set; }
        public IEnumerable<CatalogFeatureModel> Features { get; set; }
        public IEnumerable<SelectList> KeywordList { get; set; }
        public IEnumerable<string> CatalogKeywords { get; set; }
        public IEnumerable<CatalogImageModel> Images { get; set; }
        public string ImageFileName { get; set; }
    }
}