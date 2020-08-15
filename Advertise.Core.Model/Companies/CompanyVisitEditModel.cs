using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyVisitEditModel : BaseModel
    {
        public Guid CompanyId { get; set; }
        public Guid Id { get; set; }
        public bool IsVisit { get; set; }
        public Guid UserId { get; set; }
    }
}