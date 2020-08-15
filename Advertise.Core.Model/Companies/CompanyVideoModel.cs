using System;
using System.Collections.Generic;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Companies
{
    public class CompanyVideoModel : BaseModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
        public StateType State { get; set; }
        public string CompanyTitle { get; set; }
        public string CompanyMobileNumber { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyAlias { get; set; }
        public string CompanyLogoFileName { get; set; }
        public string FullName { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string FileName { get; set; }
        public IList<CompanyVideoFileModel> VideoFiles { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
