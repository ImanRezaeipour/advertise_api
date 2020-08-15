using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Products
{
    public class ProductReviewEditModel : BaseModel
    {
        public string Body { get; set; }
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public string ProductTitle { get; set; }
        public string RejectDescription { get; set; }
    }
}