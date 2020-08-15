using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Plans
{
    public class PlanPaymentSearchModel : BaseSearchModel
    {
        public Guid? CreatedById { get; set; }
    }
}