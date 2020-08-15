using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Products
{
    public class ProductReviewModel : BaseModel
    {
        public string Body { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public string ProductCode { get; set; }
        public string ProductImageFileName { get; set; }
        public string ProductTitle { get; set; }
        public string RejectDescription { get; set; }
        public StateType State { get; set; }
        public string Title { get; set; }
    }
}