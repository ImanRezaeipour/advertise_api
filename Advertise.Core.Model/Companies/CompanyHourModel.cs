using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Companies
{
    public class CompanyHourModel : BaseModel
    {
        public  DayType? DayOfWeek { get; set; }
        public TimeSpan? EndedOn { get; set; }
        public TimeSpan? StartedOn { get; set; }
    }
}