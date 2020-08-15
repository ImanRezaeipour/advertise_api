using System;
using System.Threading.Tasks;

namespace Advertise.Service.Common
{
    public interface ICommonService
    {
        Task<Guid?> GetUserIdAsync(bool isCurrentUser, Guid? userId);
        int RandomNumberByCount(int min, int max);
    }
}