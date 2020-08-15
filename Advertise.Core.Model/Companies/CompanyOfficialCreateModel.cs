using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyOfficialCreateModel : BaseModel
    {
        public string BusinessLicenseFileName { get; set; }
        public string NationalCardFileName { get; set; }
        public string OfficialNewspaperAddress { get; set; }
    }
}