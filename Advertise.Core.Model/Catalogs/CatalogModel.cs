using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Catalogs
{
    public class CatalogModel : BaseModel
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public Guid? CategoryId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Guid? ManufacturerId { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaTitle { get; set; }
        public string Title { get; set; }
        public string ImageFileName { get; set; }
    }
}