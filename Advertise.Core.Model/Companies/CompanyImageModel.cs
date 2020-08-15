using System;
using System.Collections.Generic;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Companies
{
    public class CompanyImageModel : BaseModel
    {
        public string CompanyAlias { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyLogoFileName { get; set; }
        public string CompanyMobileNumber { get; set; }
        public string CompanyTitle { get; set; }
        public DateTime CreatedOn { get; set; }
        public string FileName { get; set; }
        public string FullName { get; set; }
        public Guid Id { get; set; }
        public IEnumerable<CompanyImageFileModel> ImageFiles { get; set; }
        public int Order { get; set; }
        public string RejectDescription { get; set; }
        public StateType State { get; set; }
        public string Title { get; set; }
    }
}