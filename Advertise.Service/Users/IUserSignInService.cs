using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Advertise.Core.Domain.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Advertise.Service.Users
{
    public interface IUserSignInService
    {
        Task<ClaimsIdentity> CreateUserIdentityAsync(User user);
        string ConvertIdToString(Guid id);
        Guid ConvertIdFromString(string id);
        Task SignInAsync(User user, bool isPersistent, bool rememberBrowser);
        Task<bool> SendTwoFactorCodeAsync(string provider);
        Task<Guid> GetVerifiedUserIdAsync();
        Task<bool> HasBeenVerifiedAsync();
        Task<SignInStatus> TwoFactorSignInAsync(string provider, string code, bool isPersistent, bool rememberBrowser);
        Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo, bool isPersistent);
        Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout);
        void Dispose();
        string AuthenticationType { get; set; }
        UserManager<User, Guid> UserManager { get; set; }
        IAuthenticationManager AuthenticationManager { get; set; }
    }
}