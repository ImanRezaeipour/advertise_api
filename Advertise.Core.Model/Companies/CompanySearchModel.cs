using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Companies
{
    public class CompanySearchModel : BaseSearchModel
    {
        public string CategoryCode { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? CreatedById { get; set; }
        public StateType? State { get; set; }
        public string Title { get; set; }
    }
}