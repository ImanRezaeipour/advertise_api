using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Products
{
    public class ProductVisitModel : BaseModel
    {
        public string Body { get; set; }
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public string RejectDescription { get; set; }
        public StateType State { get; set; }
    }
}