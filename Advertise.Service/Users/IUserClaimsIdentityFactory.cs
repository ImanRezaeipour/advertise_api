using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Advertise.Core.Domain.Users;
using Microsoft.AspNet.Identity;

namespace Advertise.Service.Users
{
    public interface IUserClaimsIdentityFactory
    {
        string RoleClaimType { get; set; }
        string SecurityStampClaimType { get; set; }
        string UserIdClaimType { get; set; }
        string UserNameClaimType { get; set; }
        Task<ClaimsIdentity> CreateAsync(UserManager<User, Guid> manager, User user, string authenticationType);
    }
}