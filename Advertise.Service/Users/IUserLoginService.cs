using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Domain.Users;
using Microsoft.AspNet.Identity;

namespace Advertise.Service.Users
{
    public interface IUserLoginService
    {
        Task AddLoginAsync(User user, UserLoginInfo login);
        Task<User> FindAsync(UserLoginInfo login);
        Task<IList<UserLoginInfo>> GetLoginsAsync(User user);
        Task RemoveLoginAsync(User user, UserLoginInfo login);
    }
}