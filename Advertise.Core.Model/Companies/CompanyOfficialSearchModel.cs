using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Companies
{
    public class CompanyOfficialSearchModel : BaseSearchModel
    {
        public Guid? CompanyId { get; set; }
        public Guid? CreatedById { get; set; }
        public StateType? State { get; set; }
    }
}