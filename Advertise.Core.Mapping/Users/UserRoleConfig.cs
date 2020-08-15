using Advertise.Core.Domain.Users;
using Advertise.Core.Mapping.Common;

namespace Advertise.Core.Mapping.Users
{
    public class UserRoleConfig : BaseConfig<UserRole>
    {
        public UserRoleConfig()
        {
            HasKey(role => new
            {
                role.UserId, role.RoleId
            });
        }
    }
}