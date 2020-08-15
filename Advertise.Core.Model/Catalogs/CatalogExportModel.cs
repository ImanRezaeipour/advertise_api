using System;
using System.Collections.Generic;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Catalogs
{
    public class CatalogExportModel : BaseModel
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string FeatureTitle1 { get; set; }
        public string FeatureTitle2 { get; set; }
        public string FeatureTitle3 { get; set; }
        public string FeatureTitle4 { get; set; }
        public string FeatureTitle5 { get; set; }
        public string ImageFileName1 { get; set; }
        public string ImageFileName2 { get; set; }
        public string ImageFileName3 { get; set; }
        public string ImageFileName4 { get; set; }
        public string ImageFileName5 { get; set; }
        public string KeywordId { get; set; }
        public Guid ManufacturerId { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaTitle { get; set; }
        public string ReviewBody { get; set; }
        public string Title { get; set; }
        public IList<CatalogSpecificationModel> Specifications { get; set; }
        public Guid CategoryId { get; set; }
    }
}