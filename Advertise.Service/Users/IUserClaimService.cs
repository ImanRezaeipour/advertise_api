using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Advertise.Core.Domain.Users;

namespace Advertise.Service.Users
{
    public interface IUserClaimService
    {
        Task AddClaimAsync(User user, Claim claim);
        Task<IList<Claim>> GetClaimsAsync(User user);
        Task RemoveClaimAsync(User user, Claim claim);
    }
}