using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Catalogs
{
    public class CatalogSearchModel : BaseSearchModel
    {
        public DateTime? CreatedOn { get; set; }
        public Guid? CreatedById { get; set; }
    }
}