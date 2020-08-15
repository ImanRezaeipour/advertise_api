using System.Threading.Tasks;
using Advertise.Core.Model.Settings;

namespace Advertise.Service.Factory.Settings
{
    public interface ISettingFactory
    {
        Task<SettingEditModel> PrepareEditModel();
    }
}