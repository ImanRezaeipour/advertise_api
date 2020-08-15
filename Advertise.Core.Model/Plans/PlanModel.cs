using System;
using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Plans
{
    public class PlanModel : BaseModel
    {
        public string Code { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string DurationDay { get; set; }
        public Guid Id { get; set; }
        public decimal? Price { get; set; }
        public Guid? RoleId { get; set; }
        public IEnumerable<SelectList> RoleList { get; set; }
        public string Title { get; set; }
    }
}