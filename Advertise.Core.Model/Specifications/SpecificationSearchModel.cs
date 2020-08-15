using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Specifications
{
    public class SpecificationSearchModel : BaseSearchModel
    {
        public Guid? CategoryId { get; set; }
        public Guid? CreatedById { get; set; }
        public bool? WithParentCategory { get; set; }
    }
}