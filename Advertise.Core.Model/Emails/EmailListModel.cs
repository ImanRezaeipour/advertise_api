using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Emails
{
    public class EmailListModel : BaseModel
    {
        public IEnumerable<EmailModel> Emails { get; set; }
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public EmailSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
    }
}