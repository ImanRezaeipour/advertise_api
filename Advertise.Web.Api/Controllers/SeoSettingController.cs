using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Advertise.Core.Model.Seos;
using Advertise.Service.Factory.Seos;
using Advertise.Service.Seos;

namespace Advertise.Web.Api.Controllers
{
    public class SeoSettingController : BaseController
    {
        #region Private Fields

        private readonly ISeoSettingFactory _seoSettingFactory;
        private readonly ISeoSettingService _seoSettingService;

        #endregion Private Fields

        #region Public Constructors

        public SeoSettingController(ISeoSettingService seoSettingService, ISeoSettingFactory seoSettingFactory)
        {
            _seoSettingService = seoSettingService;
            _seoSettingFactory = seoSettingFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual async Task<HttpResponseMessage> Edit()
        {
            var viewModel = await _seoSettingFactory.PrepareEditModelAsync();
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        public virtual async Task<HttpResponseMessage> Edit(SeoSettingEditModel viewModel)
        {
            await _seoSettingService.EditByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        #endregion Public Methods
    }
}