using System.Threading.Tasks;
using Advertise.Core.Model.Users;

namespace Advertise.Service.Factory.Users
{
    public interface IUserAccountFactory
    {
        Task<UserOperatorCreateModel> PrepareUserOperatorCreateModel(UserOperatorCreateModel modelPrepare = null);
    }
}