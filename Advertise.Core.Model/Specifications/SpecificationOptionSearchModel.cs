using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Specifications
{
    public class SpecificationOptionSearchModel : BaseSearchModel
    {
        public Guid? CreatedById { get; set; }
        public Guid? SpecificationId { get; set; }
    }
}