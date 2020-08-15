using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Advertise.Core.Model.Settings;
using Advertise.Service.Factory.Settings;
using Advertise.Service.Settings;

namespace Advertise.Web.Api.Controllers
{
    public class SettingController : BaseController
    {
        #region Private Fields

        private readonly SettingFactory _settingFactory;
        private readonly SettingService _settingService;

        #endregion Private Fields

        #region Public Constructors

        public SettingController(SettingService settingService, SettingFactory settingFactory)
        {
            _settingService = settingService;
            _settingFactory = settingFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual async Task<HttpResponseMessage> Edit()
        {
            var viewModel = await _settingFactory.PrepareEditModel();
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        public virtual async Task<HttpResponseMessage> Edit(SettingEditModel viewModel)
        {
            await _settingService.EditByViewModel(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        #endregion Public Methods
    }
}