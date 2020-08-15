using System;
using System.Collections.Generic;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Specifications
{
    public class SpecificationOptionCreateModel : BaseModel
    {
        public Guid? CategoryId { get; set; }
        public Guid Id { get; set; }
        public IEnumerable<SpecificationOptionModel> Options { get; set; }
        public Guid? SpecificationId { get; set; }
    }
}