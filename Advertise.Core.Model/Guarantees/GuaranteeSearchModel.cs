using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Guarantees
{
    public class GuaranteeSearchModel : BaseSearchModel
    {
        public Guid? CreatedById { get; set; }
    }
}