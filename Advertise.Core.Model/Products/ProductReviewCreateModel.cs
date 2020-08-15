using System;
using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Products
{
    public class ProductReviewCreateModel : BaseModel
    {
        public string Body { get; set; }
        public bool IsActive { get; set; }
        public Guid ProductId { get; set; }
        public IEnumerable<SelectList> ProductList { get; set; }
    }
}