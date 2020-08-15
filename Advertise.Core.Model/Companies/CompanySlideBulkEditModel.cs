using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanySlideBulkEditModel : BaseModel
    {
        public IEnumerable<CompanySlideModel> SlideList { get; set; }
        public IEnumerable<SelectList> ProductList { get; set; }
    }
}