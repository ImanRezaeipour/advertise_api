using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Users
{
    public class UserOperatorSearchModel : BaseSearchModel
    {
        public Guid? CreatedById { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsBan { get; set; }
        public bool? IsVerify { get; set; }
        public Guid? RoleId { get; set; }
    }
}