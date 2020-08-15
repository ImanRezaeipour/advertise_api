using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Advertise.Core.Model.Announces;
using Advertise.Service.Announces;
using Advertise.Service.Factory.Announces;
using Advertise.Service.Services.Permissions;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class AnnouncePaymentController : BaseController
    {
        #region Private Fields

        private readonly IAnnouncePaymentFactory _announcePaymentFactory;
        private readonly IAnnouncePaymentService _announcePaymentService;

        #endregion Private Fields

        #region Public Constructors

        public AnnouncePaymentController(IAnnouncePaymentService announcePaymentService, IAnnouncePaymentFactory announcePaymentFactory)
        {
            _announcePaymentService = announcePaymentService;
            _announcePaymentFactory = announcePaymentFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual async Task<HttpResponseMessage> Callback(AnnouncePaymentCallbackModel viewModel)
        {
            var result = await _announcePaymentService.CallbackWithZarinpalByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [MvcAuthorize(PermissionConst.CanAdsPaymentList)]
        public virtual async Task<HttpResponseMessage> List(AnnouncePaymentSearchModel request)
        {
            var viewModel = await _announcePaymentFactory.PrepareListModel(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanAdsPaymentMyList)]
        public virtual async Task<HttpResponseMessage> MyList(AnnouncePaymentSearchModel request)
        {
            var viewModel = await _announcePaymentFactory.PrepareListModel(request, true);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        #endregion Public Methods
    }
}