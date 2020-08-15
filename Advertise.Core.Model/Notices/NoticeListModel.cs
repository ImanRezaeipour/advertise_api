using System;
using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Notices
{
    public class NoticeListModel : BaseModel
    {
        public IEnumerable<SelectList> ActiveList { get; set; }
        public string Body { get; set; }
        public DateTime CreatedOn { get; set; }
        public IEnumerable<NoticeModel> Notices { get; set; }
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public NoticeSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
        public string Title { get; set; }
    }
}