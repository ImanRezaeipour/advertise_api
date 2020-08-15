using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Companies
{
    public class CompanyQuestionSearchModel : BaseSearchModel
    {
        public Guid? CompanyId { get; set; }
        public Guid? CreatedById { get; set; }
        public Guid? ReplyId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public StateType? State { get; set; }
    }
}