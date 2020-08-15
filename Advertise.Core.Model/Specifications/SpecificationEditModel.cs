using System;
using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Specifications
{
    public class SpecificationEditModel : BaseModel
    {
        public Guid CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public string Description { get; set; }
        public Guid Id { get; set; }
        public int Order { get; set; }
        public string Title { get; set; }
        public SpecificationType Type { get; set; }
        public IEnumerable<SelectList> TypeList { get; set; }
        public IEnumerable<SpecificationOptionModel> Options { get; set; }
    }
}