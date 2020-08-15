using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Logs
{
    public class LogActivityListModel : BaseModel
    {
        public IEnumerable<LogActivityModel> ActivityLogs { get; set; }
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public LogActivitySearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
    }
}