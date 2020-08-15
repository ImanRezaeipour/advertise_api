using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Catalogs
{
    public class CatalogFeatureModel : BaseModel
    {
        public Guid? CatalogId { get; set; }
        public string Title { get; set; }
    }
}