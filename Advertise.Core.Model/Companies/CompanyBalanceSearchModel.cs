using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyBalanceSearchModel : BaseSearchModel
    {
        public Guid? CompanyId { get; set; }
        public Guid? CreatedById { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}