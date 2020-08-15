using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Roles
{
    public class RoleCreateModel : BaseModel
    {
        public string Name { get; set; }
        public string Permissions { get; set; }
    }
}