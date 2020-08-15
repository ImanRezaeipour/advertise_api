using System;
using System.Collections.Generic;
using Advertise.Core.Domain.Categories;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Manufacturers;
using Advertise.Core.Domain.Users;

namespace Advertise.Core.Domain.Catalogs
{
    public class Catalog : BaseEntity
    {
        public virtual string Body { get; set; }
        public virtual Category Category { get; set; }
        public virtual Guid? CategoryId { get; set; }
        public virtual string Code { get; set; }
        public virtual string Description { get; set; }
        public virtual ICollection<CatalogFeature> Features { get; set; }
        public virtual ICollection<CatalogImage> Images { get; set; }
        public virtual ICollection<CatalogKeyword> Keywords { get; set; }
        public virtual ICollection<CatalogLike> Likes { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public virtual Guid? ManufacturerId { get; set; }
        public virtual string MetaDescription { get; set; }
        public virtual string MetaKeywords { get; set; }
        public virtual string MetaTitle { get; set; }
        public virtual ICollection<CatalogReview> Reviews { get; set; }
        public virtual ICollection<CatalogSpecification> Specifications { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
        public virtual string Title { get; set; }
        public virtual string ImageFileName { get; set; }
    }
}