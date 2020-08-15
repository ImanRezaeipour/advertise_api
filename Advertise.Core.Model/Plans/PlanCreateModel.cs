using System;
using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Plans
{
    public class PlanCreateModel : BaseModel
    {
        public string Code { get; set; }
        public ColorType? Color { get; set; }
        public IEnumerable<SelectList> ColorTypeList { get; set; }
        public bool? IsActive { get; set; }
        public PlanDurationType? PlanDuration { get; set; }
        public IEnumerable<SelectList> PlanDurationList { get; set; }
        public decimal? PreviousePrice { get; set; }
        public decimal? Price { get; set; }
        public Guid? RoleId { get; set; }
        public IEnumerable<SelectList> RoleList { get; set; }
        public string Title { get; set; }
    }
}