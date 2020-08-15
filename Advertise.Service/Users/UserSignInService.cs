using System;
using Advertise.Core.Domain.Users;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Advertise.Service.Users
{
    public class UserSignInService : SignInManager<User, Guid>, IUserSignInService
    {
        #region Private Fields

        private readonly IAuthenticationManager _authenticationManager;
        private readonly IUserService _userService;

        #endregion Private Fields

        #region Public Constructors

        public UserSignInService(UserService userService, IAuthenticationManager authenticationManager)
            : base(userService, authenticationManager)
        {
            _userService = userService;
            _authenticationManager = authenticationManager;
        }

        #endregion Public Constructors
    }
}