using System;
using System.Collections.Generic;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyImageEditModel : BaseModel
    {
        public IEnumerable<CompanyImageFileModel> CompanyImageFile { get; set; }
        public Guid Id { get; set; }
        public int Order { get; set; }
        public string Title { get; set; }
    }
}