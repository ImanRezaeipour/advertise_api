using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Users;
using Advertise.Core.Model.Users;

namespace Advertise.Service.Users
{
    public interface IUserNotificationService {
        Task CreateByViewModelAsync();
        Task DeleteAllReadByCurrentUserAsync();
        Task DeleteByIdAsync(Guid notificationId);
        Task<UserNotification> FindByIdAsync(Guid notificationId);
        Task SeedAsync();
        Task<IList<UserNotification>> GetByRequestAsync(UserNotificationSearchModel model);
        Task<int> CountByRequestAsync(UserNotificationSearchModel model);
        IQueryable<UserNotification> QueryByRequest(UserNotificationSearchModel model);
        Task CreateAsync(Guid productId);
    }
}