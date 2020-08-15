using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Newsletters
{
    public class NewsletterListModel : BaseModel
    {
        public IEnumerable<SelectList> MeetList { get; set; }
        public IEnumerable<NewsletterModel> Newsletter { get; set; }
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public NewsletterSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
    }
}