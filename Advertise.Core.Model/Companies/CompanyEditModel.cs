using System;
using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Locations;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Companies
{
    public class CompanyEditModel : BaseModel
    {
        public LocationModel Location { get; set; }
        public Guid AddressId { get; set; }
        public IEnumerable<SelectList> AddressProvince { get; set; }
        public string Alias { get; set; }
        public string CategoryAlias { get; set; }
        public Guid CategoryId { get; set; }
        public bool CategoryRoot { get; set; }
        public string CategoryTitle { get; set; }
        public ClearingType? Clearing { get; set; }
        public IEnumerable<SelectList> ClearingList { get; set; }
        public string CoverFileName { get; set; }
        public Guid CreatedById { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public EmployeeRangeType? EmployeeRange { get; set; }
        public IEnumerable<SelectList> EmployeeRangeList { get; set; }
        public Guid Id { get; set; }
        public bool IsSetAlias { get; set; }
        public string LogoFileName { get; set; }
        public string MobileNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string RejectDescription { get; set; }
        public string ShebaNumber { get; set; }
        public string ShetabNumber { get; set; }
        public string Slogan { get; set; }
        public string Title { get; set; }
        public string WebSite { get; set; }
    }
}