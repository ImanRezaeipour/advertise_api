using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyVisitCreateModel : BaseModel
    {
        public Guid CompanyId { get; set; }
        public bool IsVisit { get; set; }
        public Guid UserId { get; set; }
    }
}