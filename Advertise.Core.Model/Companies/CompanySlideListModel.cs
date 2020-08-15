using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanySlideListModel : BaseModel
    {
        public IEnumerable<CompanySlideModel> CompanySlides { get; set; }
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public CompanySlideSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
    }
}