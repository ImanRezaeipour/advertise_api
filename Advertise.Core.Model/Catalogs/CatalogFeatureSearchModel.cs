using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Catalogs
{
    public class CatalogFeatureSearchModel : BaseSearchModel
    {
        public Guid? CatalogId { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}