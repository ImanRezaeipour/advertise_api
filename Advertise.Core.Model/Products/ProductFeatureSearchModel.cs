using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Products
{
    public class ProductFeatureSearchModel : BaseSearchModel
    {
        public Guid? CreatedById { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? ProductId { get; set; }
    }
}