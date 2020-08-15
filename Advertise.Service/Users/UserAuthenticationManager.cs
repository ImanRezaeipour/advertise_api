using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Advertise.Core.Managers.WebContext;
using Microsoft.Owin.Security;

namespace Advertise.Service.Users
{
    public class UserAuthenticationManager : IAuthenticationManager, IUserAuthenticationManager
    {
        #region Private Fields

        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public UserAuthenticationManager(IWebContextManager webContextManager)
        {
            _webContextManager = webContextManager;
        }

        #endregion Public Constructors

        #region Public Properties

        public AuthenticationResponseChallenge AuthenticationResponseChallenge
        {
            get { return _webContextManager.CurrentUserAuthenticate.AuthenticationResponseChallenge; }
            set { _webContextManager.CurrentUserAuthenticate.AuthenticationResponseChallenge = value; }
        }

        public AuthenticationResponseGrant AuthenticationResponseGrant
        {
            get { return _webContextManager.CurrentUserAuthenticate.AuthenticationResponseGrant; }
            set { _webContextManager.CurrentUserAuthenticate.AuthenticationResponseGrant = value; }
        }

        public AuthenticationResponseRevoke AuthenticationResponseRevoke
        {
            get { return _webContextManager.CurrentUserAuthenticate.AuthenticationResponseRevoke; }
            set { _webContextManager.CurrentUserAuthenticate.AuthenticationResponseRevoke = value; }
        }

        public ClaimsPrincipal User
        {
            get { return _webContextManager.CurrentUserAuthenticate.User; }
            set { _webContextManager.CurrentUserAuthenticate.User = value; }
        }

        #endregion Public Properties

        #region Public Methods

        public Task<AuthenticateResult> AuthenticateAsync(string authenticationType)
        {
            return _webContextManager.CurrentUserAuthenticate.AuthenticateAsync(authenticationType);
        }

        public Task<IEnumerable<AuthenticateResult>> AuthenticateAsync(string[] authenticationTypes)
        {
            return _webContextManager.CurrentUserAuthenticate.AuthenticateAsync(authenticationTypes);
        }

        public void Challenge(AuthenticationProperties properties, params string[] authenticationTypes)
        {
            _webContextManager.CurrentUserAuthenticate.Challenge(properties, authenticationTypes);
        }

        public void Challenge(params string[] authenticationTypes)
        {
            _webContextManager.CurrentUserAuthenticate.Challenge(authenticationTypes);
        }

        public IEnumerable<AuthenticationDescription> GetAuthenticationTypes()
        {
            return _webContextManager.CurrentUserAuthenticate.GetAuthenticationTypes();
        }

        public IEnumerable<AuthenticationDescription> GetAuthenticationTypes(Func<AuthenticationDescription, bool> predicate)
        {
            return _webContextManager.CurrentUserAuthenticate.GetAuthenticationTypes(predicate);
        }

        public void SignIn(AuthenticationProperties properties, params ClaimsIdentity[] identities)
        {
            _webContextManager.CurrentUserAuthenticate.SignIn(properties, identities);
        }

        public void SignIn(params ClaimsIdentity[] identities)
        {
            _webContextManager.CurrentUserAuthenticate.SignIn(identities);
        }

        public void SignOut(AuthenticationProperties properties, params string[] authenticationTypes)
        {
            _webContextManager.CurrentUserAuthenticate.SignOut(properties, authenticationTypes);
        }

        public void SignOut(params string[] authenticationTypes)
        {
            _webContextManager.CurrentUserAuthenticate.SignOut(authenticationTypes);
        }

        #endregion Public Methods
    }
}