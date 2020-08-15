using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Plans
{
    public class PlanDiscountSearchModel : BaseSearchModel
    {
        public Guid? CreatedById { get; set; }
        public Guid? UserId { get; set; }
    }
}