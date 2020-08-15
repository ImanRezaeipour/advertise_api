using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Complaints
{
    public class ComplaintModel : BaseModel
    {
        public string Body { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string UserAvatar { get; set; }
        public string UserFullName { get; set; }
        public string UserUserName { get; set; }
    }
}