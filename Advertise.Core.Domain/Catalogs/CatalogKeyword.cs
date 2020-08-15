using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Keywords;

namespace Advertise.Core.Domain.Catalogs
{
    public class CatalogKeyword : BaseEntity
    {
        public virtual string Title { get; set; }
        public virtual Keyword Keyword { get; set; }
        public virtual Guid? KeywordId { get; set; }
        public virtual Catalog Catalog { get; set; }
        public virtual Guid? CatalogId { get; set; }
    }
}