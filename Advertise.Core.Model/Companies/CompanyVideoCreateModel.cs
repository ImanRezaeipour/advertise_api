using System.Collections.Generic;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyVideoCreateModel : BaseModel
    {
        public IEnumerable<CompanyVideoFileModel> CompanyVideoFile { get; set; }
        public int Order { get; set; }
        public string Title { get; set; }
    }
}