using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyQuestionModel : BaseModel
    {
        public string Body { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid Id { get; set; }
        public Guid? ReplyId { get; set; }
        public bool InitLike { get; set; }
        public int LikeCount { get; set; }
        public string CompanyImageFileName { get; set; }
        public string CompanyTitle { get; set; }
        public string CompanyAlias { get; set; }
        public bool IsMyself { get; set; }
        public string UserAvatar { get; set; }
        public string UserDisplayName { get; set; }
        public string UserFullName { get; set; }
        public string UserUserName { get; set; }
    }
}