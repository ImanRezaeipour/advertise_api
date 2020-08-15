using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Products
{
    public class ProductLikeEditModel : BaseModel
    {
        public Guid Id { get; set; }
        public bool IsFollow { get; set; }
    }
}