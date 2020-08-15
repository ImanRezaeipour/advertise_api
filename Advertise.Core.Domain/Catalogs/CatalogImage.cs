using System;
using Advertise.Core.Domain.Common;

namespace Advertise.Core.Domain.Catalogs
{
    public class CatalogImage : BaseImage
    {
        public virtual int? Order { get; set; }
        public virtual bool? IsWatermark { get; set; }
        public virtual Catalog Catalog { get; set; }
        public virtual Guid? CatalogId { get; set; }
    }
}