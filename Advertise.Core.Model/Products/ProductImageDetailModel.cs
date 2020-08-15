using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Products
{
    public class ProductImageDetailModel : BaseModel
    {
        public string FileName { get; set; }
        public Guid Id { get; set; }
        public int Order { get; set; }
    }
}