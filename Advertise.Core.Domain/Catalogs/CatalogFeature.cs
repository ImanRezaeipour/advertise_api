using System;
using Advertise.Core.Domain.Common;

namespace Advertise.Core.Domain.Catalogs
{
    public class CatalogFeature : BaseEntity
    {
        public virtual string Title { get; set; }
        public virtual Catalog Catalog { get; set; }
        public virtual Guid? CatalogId { get; set; }
    }
}