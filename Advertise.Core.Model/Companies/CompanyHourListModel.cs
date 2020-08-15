using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyHourListModel : BaseModel
    {
        public IEnumerable<CompanyHourModel> CompanyHours { get; set; }
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public CompanyHourSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
    }
}