using System;
using System.Collections.Generic;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Companies
{
    public class CompanyAttachmentDetailModel : BaseModel
    {
        public Guid Id { get; set; }
        public int Order { get; set; }
        public string RejectDescription { get; set; }
        public StateType State { get; set; }
        public string Title { get; set; }
        public bool IsMyself { get; set; }
        public IEnumerable<CompanyAttachmentFileModel> Files { get; set; }
        public string CompanyLogo { get; set; }
        public string CompanyAlias { get; set; }
        public string CompanyTitle { get; set; }
    }
}