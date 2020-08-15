using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Roles
{
    public class RoleModel : BaseModel
    {
        public DateTime CreatedOn { get; set; }
        public Guid Id { get; set; }
        public bool IsBan { get; set; }
        public bool IsSystemRole { get; set; }
        public string Name { get; set; }
        public string Permissions { get; set; }
    }
}