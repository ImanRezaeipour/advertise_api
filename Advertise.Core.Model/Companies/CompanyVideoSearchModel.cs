using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Companies
{
    public class CompanyVideoSearchModel : BaseSearchModel
    {
        public Guid? CompanyId { get; set; }
        public Guid? CreatedById { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? Id { get; set; }
        public StateType? State { get; set; }
        public Guid? UserId { get; set; }
    }
}