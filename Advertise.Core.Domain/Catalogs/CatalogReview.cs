using System;
using Advertise.Core.Domain.Common;

namespace Advertise.Core.Domain.Catalogs
{
    public class CatalogReview : BaseReview
    {
        public virtual Catalog Catalog { get; set; }
        public virtual Guid? CatalogId { get; set; }
    }
}