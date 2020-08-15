using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Advertise.Service.Factory.Common;
using Advertise.Service.Services.Permissions;
using Advertise.Web.Filters;
using Advertise.Web.Results;

namespace Advertise.Web.Api.Controllers
{
    public class CommonController : BaseController
    {
        #region Private Fields

        private readonly ICommonFactory _commonFactory;

        #endregion Private Fields

        #region Public Constructors

        public CommonController(ICommonFactory commonFactory)
        {
            _commonFactory = commonFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        [NoBrowserCache]
        [SkipRemoveWhiteSpaces]
        public virtual HttpResponseMessage CaptchaImage(string rndDate)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new CaptchaImageResult());
        }

        public virtual async Task<HttpResponseMessage> LandingPage()
        {
            var viewModel = await _commonFactory.PrepareLandingPageModelAsync();
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }
        
        [MvcAuthorize(PermissionConst.CanHomeBoardPage)]
        public virtual async Task<HttpResponseMessage> DashBoard()
        {
            var viewModel = await _commonFactory.PrepareDashBoardModelAsync();
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }
        
        public virtual async Task<HttpResponseMessage> Profile()
        {
            var viewModel = await _commonFactory.PrepareProfileViewModelAsync();
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        #endregion Public Methods
    }
}