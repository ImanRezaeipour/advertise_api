using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyQuestionCreateModel : BaseModel
    {
        public string Body { get; set; }
        public Guid CompanyId { get; set; }
        public Guid? ReplyId { get; set; }
    }
}