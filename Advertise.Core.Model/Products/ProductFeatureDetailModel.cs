using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Products
{
    public class ProductFeatureDetailModel : BaseModel
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
    }
}