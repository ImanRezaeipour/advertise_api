using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyConversationSearchModel : BaseSearchModel
    {
        public Guid? CreatedById { get; set; }
        public Guid? RecivedById { get; set; }
    }
}