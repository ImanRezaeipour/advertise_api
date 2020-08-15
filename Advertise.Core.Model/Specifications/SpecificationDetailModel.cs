using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Specifications
{
    public class SpecificationDetailModel : BaseModel
    {
        public virtual Guid CategoryId { get; set; }
        public string Description { get; set; }
        public Guid Id { get; set; }
        public int Order { get; set; }
        public string Title { get; set; }
        public SpecificationType Type { get; set; }
    }
}