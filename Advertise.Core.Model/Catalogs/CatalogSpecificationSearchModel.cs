using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Catalogs
{
    public class CatalogSpecificationSearchModel : BaseSearchModel
    {
        public Guid? CatalogId { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}