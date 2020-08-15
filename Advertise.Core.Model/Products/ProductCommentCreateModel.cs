using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Products
{
    public class ProductCommentCreateModel : BaseModel
    {
        public string Body { get; set; }
        public Guid ProductId { get; set; }
    }
}