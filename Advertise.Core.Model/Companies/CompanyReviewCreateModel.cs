using System;
using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyReviewCreateModel : BaseModel
    {
        public string Body { get; set; }
        public Guid CompanyId { get; set; }
        public IEnumerable<SelectList> CompanyList { get; set; }
        public bool IsActive { get; set; }
        public string Title { get; set; }
    }
}