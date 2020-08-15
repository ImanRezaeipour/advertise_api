using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Products
{
    public class ProductCommentEditModel : BaseModel
    {
        public string Body { get; set; }
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
    }
}