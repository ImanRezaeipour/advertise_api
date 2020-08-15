using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Roles
{
    public class RoleEditModel : BaseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Permissions { get; set; }
    }
}