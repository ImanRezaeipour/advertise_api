using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;

namespace Advertise.Service.Users
{
    public interface IUserAuthenticationManager
    {
        AuthenticationResponseChallenge AuthenticationResponseChallenge { get; set; }
        AuthenticationResponseGrant AuthenticationResponseGrant { get; set; }
        AuthenticationResponseRevoke AuthenticationResponseRevoke { get; set; }
        ClaimsPrincipal User { get; set; }
        Task<AuthenticateResult> AuthenticateAsync(string authenticationType);
        Task<IEnumerable<AuthenticateResult>> AuthenticateAsync(string[] authenticationTypes);
        void Challenge(AuthenticationProperties properties, params string[] authenticationTypes);
        void Challenge(params string[] authenticationTypes);
        IEnumerable<AuthenticationDescription> GetAuthenticationTypes();
        IEnumerable<AuthenticationDescription> GetAuthenticationTypes(Func<AuthenticationDescription, bool> predicate);
        void SignIn(AuthenticationProperties properties, params ClaimsIdentity[] identities);
        void SignIn(params ClaimsIdentity[] identities);
        void SignOut(AuthenticationProperties properties, params string[] authenticationTypes);
        void SignOut(params string[] authenticationTypes);
    }
}