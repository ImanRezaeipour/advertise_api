using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Users;
using Advertise.Core.Model.Users;

namespace Advertise.Service.Users
{
    public interface IUserOnlineService
    {
        Task<int> CountAllAsync();
        Task<int> CountByRequestAsync(UserOnlineSearchModel model);
        void CreateByViewModel(UserOnlineModel model);
        void DeleteBySessionId(string sessionId);
        IQueryable<UserOnline> QueryByRequest(UserOnlineSearchModel model);
    }
}