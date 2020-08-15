using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Users
{
    public class UserViolationSearchModel : BaseSearchModel
    {
        public Guid? CreatedById { get; set; }
        public bool? IsRead { get; set; }
        public ReasonType? ReasonType { get; set; }
        public ReportType? ReportType { get; set; }
    }
}