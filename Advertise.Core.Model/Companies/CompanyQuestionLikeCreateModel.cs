using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyQuestionLikeCreateModel : BaseModel
    {
        public Guid CompanyQuestionId { get; set; }
        public bool IsLike { get; set; }
        public Guid UserId { get; set; }
    }
}