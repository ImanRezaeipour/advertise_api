using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyOfficialModel : BaseModel
    {
        public string BusinessLicenseFileName { get; set; }
        public bool IsApprove { get; set; }
        public bool IsComplete { get; set; }
        public string CompanyTitle { get; set; }
        public string CompanyAlias { get; set; }
        public Guid Id { get; set; }
        public string NationalCardFileName { get; set; }
        public string OfficialNewspaperAddress { get; set; }
    }
}