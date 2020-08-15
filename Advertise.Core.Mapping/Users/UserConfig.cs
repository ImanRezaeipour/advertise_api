using Advertise.Core.Domain.Users;
using Advertise.Core.Mapping.Common;

namespace Advertise.Core.Mapping.Users
{
    public class UserConfig : BaseConfig<User>
    {
        public UserConfig()
        {
            HasMany(user => user.Roles).WithRequired().HasForeignKey(role => role.UserId);
            HasMany(user => user.Claims).WithRequired().HasForeignKey(claim => claim.UserId);
            HasMany(user => user.Logins).WithRequired().HasForeignKey(login => login.UserId);
            HasRequired(user => user.CreatedBy).WithMany().HasForeignKey(user => user.CreatedById);
            HasOptional(a => a.Meta).WithMany().HasForeignKey(a => a.MetaId);
            HasOptional(a => a.Setting).WithMany().HasForeignKey(a => a.SettingId);
            Property(user => user.DirectPermissions).HasColumnType("xml");
            Ignore(user => user.XmlDirectPermissions);
        }
    }
}