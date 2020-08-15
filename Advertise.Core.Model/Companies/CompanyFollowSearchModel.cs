using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyFollowSearchModel : BaseSearchModel
    {
        public Guid? CompanyId { get; set; }
        public Guid? FollowedById { get; set; }
        public bool? IsFollow { get; set; }
    }
}