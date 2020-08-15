using System;
using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyConversationListModel : BaseModel
    {
        public IEnumerable<CompanyConversationModel> CompanyConversationList { get; set; }
        public IEnumerable<CompanyConversationModel> Conversations { get; set; }
        public IEnumerable<SelectList> CreatedIdConversationList { get; set; }
        public Guid OwnedById { get; set; }
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public CompanyConversationSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
    }
}