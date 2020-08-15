using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyReviewEditModel : BaseModel
    {
        public string Body { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyTitle { get; set; }
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public string Title { get; set; }
    }
}