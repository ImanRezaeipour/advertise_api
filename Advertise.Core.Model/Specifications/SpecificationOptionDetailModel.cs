using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Specifications
{
    public class SpecificationOptionDetailModel : BaseModel
    {
        public string Description { get; set; }
        public Guid Id { get; set; }
        public Guid SpecificationId { get; set; }
        public string Title { get; set; }
    }
}