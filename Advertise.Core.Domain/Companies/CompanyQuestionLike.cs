using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Users;

namespace Advertise.Core.Domain.Companies
{
    public class CompanyQuestionLike : BaseEntity
    {
        public virtual bool? IsLike { get; set; }
        public virtual User LikedBy { get; set; }
        public virtual Guid? LikedById { get; set; }
        public virtual CompanyQuestion Question { get; set; }
        public virtual Guid? QuestionId { get; set; }
    }
}