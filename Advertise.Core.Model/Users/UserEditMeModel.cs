using System;
using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Locations;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Users
{
    public class UserEditMeModel : BaseModel
    {
        public LocationModel Location { get; set; }
        public IEnumerable<SelectList> AddressProvince { get; set; }
        public string AvatarFileName { get; set; }
        public string FirstName { get; set; }
        public GenderType? Gender { get; set; }
        public IEnumerable<SelectList> GenderList { get; set; }
        public string HomeNumber { get; set; }
        public Guid Id { get; set; }
        public bool IsSetUserName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string OrginalUserName { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
    }
}