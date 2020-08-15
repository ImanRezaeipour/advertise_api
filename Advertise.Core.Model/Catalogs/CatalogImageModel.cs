using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Catalogs
{
    public class CatalogImageModel : BaseModel
    {
        public string FileName { get; set; }
        public Guid? CatalogId { get; set; }
        public bool? IsWatermark { get; set; }
        public int? Order { get; set; }
    }
}