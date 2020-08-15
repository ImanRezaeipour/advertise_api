using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyQuestionListModel : BaseModel
    {
        public IEnumerable<CompanyQuestionModel> CompanyQuestions { get; set; }
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public CompanyQuestionSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> StateTypeList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
        public bool? IsMySelf { get; set; }
    }
}