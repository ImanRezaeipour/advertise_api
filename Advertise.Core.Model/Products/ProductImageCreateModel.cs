using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Products
{
    public class ProductImageCreateModel : BaseModel
    {
        public string FileName { get; set; }
        public Guid Id { get; set; }
        public bool IsWatermark { get; set; }
        public int Order { get; set; }
    }
}