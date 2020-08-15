using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Logs
{
    public class LogAuditSearchModel : BaseSearchModel
    {
        public Guid? CreatedById { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}