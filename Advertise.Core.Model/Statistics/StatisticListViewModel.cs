using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Statistics
{
    public class StatisticListViewModel
    {
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public StatisticSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
        public IEnumerable<StatisticModel> Statistics { get; set; }
    }
}