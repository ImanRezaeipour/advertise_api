using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Products
{
    public class ProductReviewSearchModel : BaseSearchModel
    {
        public Guid? CreatedById { get; set; }
        public bool? IsActive { get; set; }
        public string ProductCode { get; set; }
        public Guid? ProductId { get; set; }
        public StateType? State { get; set; }
    }
}