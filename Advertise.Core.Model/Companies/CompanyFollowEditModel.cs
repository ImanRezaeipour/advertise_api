using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyFollowEditModel : BaseModel
    {
        public Guid Id { get; set; }
        public bool IsFollow { get; set; }
    }
}