using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Products
{
    public class ProductCommentDetailModel : BaseModel
    {
        public string Body { get; set; }
        public Guid Id { get; set; }
        public StateType Status { get; set; }
    }
}