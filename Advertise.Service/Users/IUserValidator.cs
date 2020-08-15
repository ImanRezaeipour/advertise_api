using System.Threading.Tasks;
using Advertise.Core.Domain.Users;
using Microsoft.AspNet.Identity;

namespace Advertise.Service.Users
{
    public interface IUserValidator
    {
        bool AllowOnlyAlphanumericUserNames { get; set; }
        bool RequireUniqueEmail { get; set; }
        Task<IdentityResult> ValidateAsync(User item);
    }
}