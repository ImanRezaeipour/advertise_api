using System.Threading.Tasks;
using Advertise.Core.Domain.Settings;
using Advertise.Core.Model.Settings;

namespace Advertise.Service.Settings
{
    public interface ISettingService
    {
        Task EditByViewModel(SettingEditModel model);
        SettingModel GetFirst();
        Task<Setting> GetFirstAsync();
    }
}