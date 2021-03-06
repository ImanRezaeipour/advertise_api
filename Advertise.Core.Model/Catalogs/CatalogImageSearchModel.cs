using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Catalogs
{
    public class CatalogImageSearchModel : BaseSearchModel
    {
        public DateTime? CreatedOn { get; set; }
        public Guid? CatalogId { get; set; }
    }
}