using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Logs
{
    public class LogAuditListModel : BaseModel
    {
        public IEnumerable<LogAuditModel> AuditLogs { get; set; }
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public LogAuditSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
    }
}