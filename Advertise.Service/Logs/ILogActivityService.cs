using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Logs;
using Advertise.Core.Model.Logs;

namespace Advertise.Service.Logs
{
    public interface ILogActivityService
    {
        Task<int> CountByRequestAsync(LogActivitySearchModel request);
        Task CreateByViewModelAsync(LogActivityCreateModel viewModel);
        Task DeleteByIdAsync(Guid activityLogId);
        Task EditByViewModelAsync(LogActivityEditModel viewModel);
        Task<LogActivity> FindByIdAsync(Guid activityLogId);
        Task<IList<LogActivity>> GetByRequestAsync(LogActivitySearchModel request);
        Task<LogActivityListModel> ListByRequestAsync(LogActivitySearchModel request, bool isCurrentUser = false, Guid? userId = null);
        IQueryable<LogActivity> QueryByRequest(LogActivitySearchModel request);
        Task SeedAsync();
    }
}