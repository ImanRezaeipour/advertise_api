using System;
using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyTagListModel : BaseModel
    {
        public IEnumerable<CompanyTagModel> CompanyTags { get; set; }
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public CompanyTagSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
        public bool IsMyself { get; set; }
    }
}