using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyImageFileModel : BaseModel
    {
        public string FileName { get; set; }
        public Guid Id { get; set; }
    }
}