using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Companies
{
    public class CompanyHourCreateModel : BaseModel
    {
        public  DayType? DayOfWeek { get; set; }
        public  TimeSpan? EndTimeFirst { get; set; }
        public  TimeSpan? StartTimeFirst { get; set; }
        public  TimeSpan? EndTimeSecond { get; set; }
        public  TimeSpan? StartTimeSecond { get; set; }
        public  Guid? CompanyId { get; set; }
    }
}