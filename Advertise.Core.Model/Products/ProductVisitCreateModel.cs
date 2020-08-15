using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Products
{
    public class ProductVisitCreateModel : BaseModel
    {
        public bool IsVisit { get; set; }
        public Guid ProductId { get; set; }
    }
}