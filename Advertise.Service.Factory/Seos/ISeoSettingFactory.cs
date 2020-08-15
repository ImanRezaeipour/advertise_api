using System.Threading.Tasks;
using Advertise.Core.Model.Seos;

namespace Advertise.Service.Factory.Seos
{
    public interface ISeoSettingFactory
    {
        Task<SeoSettingEditModel> PrepareEditModelAsync(SeoSettingEditModel modelPrepare = null);
    }
}