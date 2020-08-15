using System.Threading.Tasks;
using Advertise.Core.Model.Settings;
using Advertise.Service.Settings;
using AutoMapper;

namespace Advertise.Service.Factory.Settings
{
    public class SettingFactory : ISettingFactory
    {
        #region Private Fields

        private readonly IMapper _mapper;
        private readonly SettingService _settingService;

        #endregion Private Fields

        #region Public Constructors

        public SettingFactory(SettingService settingService, IMapper mapper)
        {
            _settingService = settingService;
            _mapper = mapper;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<SettingEditModel> PrepareEditModel()
        {
            var setting = await _settingService.GetFirstAsync();
            var viewModel = setting != null ? _mapper.Map<SettingEditModel>(setting) : new SettingEditModel();

            return viewModel;
        }

        #endregion Public Methods
    }
}