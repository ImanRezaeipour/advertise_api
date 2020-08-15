using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Companies
{
    public class CompanyCreateModel : BaseModel
    {
        public string Alias { get; set; }
        public Guid CategoryId { get; set; }
        public Guid CreatedById { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Title { get; set; }
        public EmployeeRangeType? EmployeeRange { get; set; }
    }
}