using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyQuestionLikeModel : BaseModel
    {
        public string BrandName { get; set; }
        public Guid CompanyId { get; set; }
        public Guid Id { get; set; }
        public bool IsLike { get; set; }
        public string LogoFileName { get; set; }
        public string PhoneNumber { get; set; }
    }
}