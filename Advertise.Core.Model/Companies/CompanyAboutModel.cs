using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyAboutModel : BaseModel
    {
        public string BrandName { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public long EmployeeCount { get; set; }
        public DateTime EstablishedOn { get; set; }
        public Guid Id { get; set; }
        public string LogoFileName { get; set; }
        public string MobileNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string WebSite { get; set; }
    }
}