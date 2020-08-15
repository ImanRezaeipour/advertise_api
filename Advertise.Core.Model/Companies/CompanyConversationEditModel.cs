using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyConversationEditModel : BaseModel
    {
        public string Body { get; set; }
        public Guid Id { get; set; }
        public Guid? ReceivedById { get; set; }
        public Guid? ReplyId { get; set; }
    }
}