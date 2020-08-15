using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Users;
using Advertise.Core.Model.Users;
using Advertise.Service.Common;

namespace Advertise.Service.Users
{
    public interface IUserSettingService
    {
        Task<int> CountByRequestAsync(UserSettingSearchModel model);
        Task CreateByUserIdAsync(Guid userId);
        Task CreateByViewModelAsync(UserSettingCreateModel viewModel);
        Task DeleteByIdAsync(Guid userSettingId);
        Task EditByViewModelAsync(UserSettingEditModel model, bool applyCurrentUser = false);
        Task<UserSetting> FindByIdAsync(Guid userSettingId);
        Task<UserSetting> FindByUserIdAsync(Guid userId);
        Task<IList<UserSetting>> GetByRequestAsync(UserSettingSearchModel model);
        Task<string> GetMyLanguageAsync();
        IQueryable<UserSetting> QueryByRequest(UserSettingSearchModel model);
        Task<ServiceResult> SeedAsync();
    }
}