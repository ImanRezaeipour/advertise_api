using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Logs
{
    public class LogActivitySearchModel : BaseSearchModel
    {
        public Guid? CreatedById { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}