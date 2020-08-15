using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Users;

namespace Advertise.Core.Domain.Catalogs
{
    public class CatalogLike : BaseEntity
    {
        public virtual bool? IsLike { get; set; }
        public virtual User LikedBy { get; set; }
        public virtual Guid? LikedById { get; set; }
        public virtual Catalog Catalog { get; set; }
        public virtual Guid? CatalogId { get; set; }
    }
}