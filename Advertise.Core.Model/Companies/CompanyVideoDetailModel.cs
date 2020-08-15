using System.Collections.Generic;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyVideoDetailModel : BaseModel
    {
        public  string Title { get; set; }
        public  string CompanyTitle { get; set; }
        public  string CompanyLogo { get; set; }
        public  string CompanyAlias { get; set; }
        public  int? Order { get; set; }
        public IEnumerable<CompanyVideoFileModel> VideoFiles { get; set; }
        public IEnumerable<CompanyVideoModel> Galleries { get; set; }
        public bool IsMySelf { get; set; }
    }
}
