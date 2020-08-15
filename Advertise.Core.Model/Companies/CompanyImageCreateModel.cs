using System.Collections.Generic;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyImageCreateModel : BaseModel
    {
        public IEnumerable<CompanyImageFileModel> CompanyImageFile { get; set; }
        public int Order { get; set; }
        public string Title { get; set; }
    }
}