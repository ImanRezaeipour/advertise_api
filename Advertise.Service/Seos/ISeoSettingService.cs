using System.Threading.Tasks;
using Advertise.Core.Domain.Seos;
using Advertise.Core.Model.Seos;

namespace Advertise.Service.Seos
{
    public interface ISeoSettingService
    {
        Task EditByViewModelAsync(SeoSettingEditModel model);
        SeoSettingModel GetFirst();
        Task<SeoSetting> GetFirstAsync();
    }
}