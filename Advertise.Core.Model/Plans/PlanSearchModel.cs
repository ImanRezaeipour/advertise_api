using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Plans
{
    public class PlanSearchModel : BaseSearchModel
    {
        public Guid? CreatedById { get; set; }
        public Guid? UserId { get; set; }
    }
}