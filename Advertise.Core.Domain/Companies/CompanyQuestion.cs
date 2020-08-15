using System;
using System.Collections.Generic;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Users;
using Advertise.Core.Types;

namespace Advertise.Core.Domain.Companies
{
    public class CompanyQuestion : BaseEntity
    {
        public virtual string Title { get; set; }
        public virtual string Body { get; set; }
        public virtual StateType? State { get; set; }
        public virtual string RejectDescription { get; set; }
        public virtual CompanyQuestion Reply { get; set; }
        public virtual Guid? ReplyId { get; set; }
        public virtual Company Company { get; set; }
        public virtual Guid? CompanyId { get; set; }
        public virtual User QuestionedBy { get; set; }
        public virtual Guid? QuestionedById { get; set; }
        public virtual User ApprovedBy { get; set; }
        public virtual Guid? ApprovedById { get; set; }
        public virtual ICollection<CompanyQuestion> Childrens { get; set; }
        public virtual ICollection<CompanyQuestionLike> Likes { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
        public virtual User ModifiedBy { get; set; }
        public virtual Guid? ModifiedById { get; set; }
    }
}