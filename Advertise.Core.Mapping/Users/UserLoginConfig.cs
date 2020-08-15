using Advertise.Core.Domain.Users;
using Advertise.Core.Mapping.Common;

namespace Advertise.Core.Mapping.Users
{
    public class UserLoginConfig : BaseConfig<UserLogin>
    {
        public UserLoginConfig()
        {
            HasKey(login => new {login.LoginProvider, login.ProviderKey, login.UserId});
        }
    }
}