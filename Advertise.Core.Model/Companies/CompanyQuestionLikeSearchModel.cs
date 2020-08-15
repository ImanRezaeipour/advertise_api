using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyQuestionLikeSearchModel : BaseSearchModel
    {
        public bool? IsLike { get; set; }
        public Guid? LikedById { get; set; }
        public Guid? QuestionId { get; set; }
    }
}