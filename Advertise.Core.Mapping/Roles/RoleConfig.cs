using Advertise.Core.Domain.Roles;
using Advertise.Core.Mapping.Common;

namespace Advertise.Core.Mapping.Roles
{
    public class RoleConfig : BaseConfig<Role>
    {
        public RoleConfig()
        {
            Property(role => role.Permissions).HasColumnType("xml");
            Ignore(r => r.XmlPermissions);
        }
    }
}