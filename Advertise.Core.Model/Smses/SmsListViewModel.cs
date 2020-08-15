using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Smses
{
    public class SmsListViewModel
    {
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public SmsSearchModel SearchModel { get; set; }
        public IEnumerable<SmsModel> Smses { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
    }
}