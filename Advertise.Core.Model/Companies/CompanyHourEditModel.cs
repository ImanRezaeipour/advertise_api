using System;
using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyHourEditModel : BaseModel
    {
       public IEnumerable<CompanyHourModel> CompanyHours { get; set; }
       public IEnumerable<SelectList> DayList { get; set; }
       public Guid CompanyId { get; set; } 
    }
}