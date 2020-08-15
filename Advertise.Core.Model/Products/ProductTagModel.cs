using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Products
{
    public class ProductTagModel : BaseModel
    {
        public Guid Id { get; set; }
        public ColorType TagColor { get; set; }
        public string TagTitle { get; set; }
    }
}