using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Catalogs
{
    public class CatalogKeywordModel : BaseModel
    {
        public Guid? CatalogId { get; set; }
        public Guid? KeywordId { get; set; }
        public string Title { get; set; }
    }
}