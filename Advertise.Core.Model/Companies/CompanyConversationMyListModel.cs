using System;
using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyConversationMyListModel : BaseModel
    {
        public IEnumerable<CompanyConversationModel> CompanyConversationList { get; set; }
        public IEnumerable<SelectList> CreatedIdConversationList { get; set; }
        public Guid OwnedById { get; set; }
    }
}