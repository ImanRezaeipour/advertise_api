using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Companies
{
    public class CompanyAttachmentFileSearchModel : BaseSearchModel
    {
        public Guid? CompanyAttachmentId { get; set; }
        public Guid? Id { get; set; }
        public Guid? CreatedById { get; set; }
        public StateType? State { get; set; }
    }
}