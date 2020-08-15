using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyAttachmentListModel : BaseModel
    {
        public IEnumerable<CompanyAttachmentModel> CompanyAttachments { get; set; }
        public IEnumerable<SelectList> CompanyList { get; set; }
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public CompanyAttachmentSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
        public IEnumerable<SelectList> StateList { get; set; }
        public IEnumerable<SelectList> UserList { get; set; }
        public bool IsMyself { get; set; }
    }
}