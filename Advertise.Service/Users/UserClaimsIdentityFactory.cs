using System;
using System.Data.Entity.Utilities;
using System.Security.Claims;
using System.Threading.Tasks;
using Advertise.Core.Domain.Users;
using Microsoft.AspNet.Identity;

namespace Advertise.Service.Users
{
    public class UserClaimsIdentityFactory : IClaimsIdentityFactory<User, Guid>, IUserClaimsIdentityFactory
    {
        #region Public Constructors

        public UserClaimsIdentityFactory()
        {
            RoleClaimType = ClaimsIdentity.DefaultRoleClaimType;
            UserIdClaimType = ClaimTypes.NameIdentifier;
            UserNameClaimType = ClaimsIdentity.DefaultNameClaimType;
            SecurityStampClaimType = Constants.DefaultSecurityStampClaimType;
        }

        #endregion Public Constructors

        #region Public Properties

        public string RoleClaimType { get; set; }

        public string SecurityStampClaimType { get; set; }

        public string UserIdClaimType { get; set; }

        public string UserNameClaimType { get; set; }

        #endregion Public Properties

        #region Public Methods

        public virtual async Task<ClaimsIdentity> CreateAsync(UserManager<User, Guid> manager, User user, string authenticationType)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var id = new ClaimsIdentity(authenticationType, UserNameClaimType, RoleClaimType);
            id.AddClaim(new Claim(UserIdClaimType, user.Id.ToString(), ClaimValueTypes.String));
            id.AddClaim(new Claim(UserNameClaimType, user.UserName, ClaimValueTypes.String));
            id.AddClaim(new Claim(UserClaimsIdentityConst.IdentityProviderClaimType, UserClaimsIdentityConst.DefaultIdentityProviderClaimValue, ClaimValueTypes.String));
            if (manager.SupportsUserSecurityStamp)
            {
                id.AddClaim(new Claim(SecurityStampClaimType,
                    await manager.GetSecurityStampAsync(user.Id).WithCurrentCulture()));
            }
            if (manager.SupportsUserRole)
            {
                var roles = await manager.GetRolesAsync(user.Id).WithCurrentCulture();
                foreach (var roleName in roles)
                {
                    id.AddClaim(new Claim(RoleClaimType, roleName, ClaimValueTypes.String));
                }
            }
            if (manager.SupportsUserClaim)
            {
                id.AddClaims(await manager.GetClaimsAsync(user.Id).WithCurrentCulture());
            }
            return id;
        }

        #endregion Public Methods
    }
}