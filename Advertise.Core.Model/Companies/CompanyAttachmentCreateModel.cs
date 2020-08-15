using System.Collections.Generic;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyAttachmentCreateModel : BaseModel
    {
        public IEnumerable<CompanyAttachmentFileModel> CompanyAttachmentFile { get; set; }
        public int Order { get; set; }
        public string Title { get; set; }
    }
}