using System.Threading.Tasks;
using Advertise.Core.Helpers;
using Advertise.Core.Model.Seos;
using Advertise.Core.Types;
using Advertise.Service.Common;
using Advertise.Service.Seos;
using AutoMapper;

namespace Advertise.Service.Factory.Seos
{
    public class SeoSettingFactory : ISeoSettingFactory
    {
        #region Private Fields

        private readonly IListService _listService;
        private readonly IMapper _mapper;
        private readonly ISeoSettingService _seoSettingService;

        #endregion Private Fields

        #region Public Constructors

        public SeoSettingFactory(ISeoSettingService seoSettingService, IMapper mapper, IListService listService)
        {
            _seoSettingService = seoSettingService;
            _mapper = mapper;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<SeoSettingEditModel> PrepareEditModelAsync(SeoSettingEditModel modelPrepare = null)
        {
            var seoSetting = await _seoSettingService.GetFirstAsync();
            var viewModel =modelPrepare ??( seoSetting != null ? _mapper.Map<SeoSettingEditModel>(seoSetting) : new SeoSettingEditModel());
            viewModel.WwwRequirementList = EnumHelper.CastToSelectListItems<ActiveType>();

            return viewModel;
        }

        #endregion Public Methods
    }
}