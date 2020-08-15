using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Products
{
    public class ProductImageSearchModel : BaseSearchModel
    {
        public Guid? CreatedById { get; set; }
        public Guid? ProductId { get; set; }
    }
}