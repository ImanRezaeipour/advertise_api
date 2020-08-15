using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Companies
{
    public class CompanyAttachmentSearchModel : BaseSearchModel
    {
        public Guid? CompanyId { get; set; }
        public Guid? Id { get; set; }
        public Guid? CreatedById { get; set; }
        public DateTime? CreatedOn { get; set; }
        public StateType? State { get; set; }
        public string Title { get; set; }
        public Guid? UserId { get; set; }
    }
}