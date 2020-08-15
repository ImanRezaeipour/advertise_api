using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Locations;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Companies
{
    public class CompanyRegisterModel : BaseModel
    {
        public LocationModel Location { get; set; }
        public Guid CreatedById { get; set; }
        public string Email { get; set; }
        public Guid Id { get; set; }
        public StateType State { get; set; }
        public string Title { get; set; }
    }
}