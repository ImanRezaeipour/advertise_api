using System;
using System.Collections.Generic;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyAttachmentEditModel : BaseModel
    {
        public IEnumerable<CompanyAttachmentFileModel> CompanyAttachmentFile { get; set; }
        public Guid Id { get; set; }
        public int Order { get; set; }
        public string Title { get; set; }
    }
}