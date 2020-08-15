using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Products
{
    public class ProductCommentLikeModel : BaseModel
    {
        public string Body { get; set; }
        public string BrandName { get; set; }
        public string CreatorFullName { get; set; }
        public string EditorFullName { get; set; }
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public StateType Status { get; set; }
    }
}