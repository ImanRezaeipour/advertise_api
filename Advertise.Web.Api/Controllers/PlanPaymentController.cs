using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Advertise.Core.Model.Plans;
using Advertise.Service.Factory.Plans;
using Advertise.Service.Plans;

namespace Advertise.Web.Api.Controllers
{

    public class PlanPaymentController : BaseController
    {
        #region Private Fields

        private readonly IPlanPaymentFactory _planPaymentFactory;
        private readonly IPlanPaymentService _planPaymentService;

        #endregion Private Fields

        #region Public Constructors

        public PlanPaymentController(IPlanPaymentService planPaymentService, IPlanPaymentFactory planPaymentFactory)
        {
            _planPaymentService = planPaymentService;
            _planPaymentFactory = planPaymentFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual async Task<HttpResponseMessage> Callback(PlanPaymentCallbackModel viewModel)
        {
            await _planPaymentService.CallbackByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public virtual async Task<HttpResponseMessage> Create(PlanPyamentCreateModel viewModel)
        {
            var result = await _planPaymentService.CreateByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        public virtual async Task<HttpResponseMessage> Create()
        {
            var viewModel = await _planPaymentFactory.PrepareCreateModel();
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        public virtual async Task<HttpResponseMessage> List(PlanPaymentSearchModel request)
        {
            var viewModel = await _planPaymentFactory.PrepareListModel(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }
 
        public virtual async Task<HttpResponseMessage> MyList(PlanPaymentSearchModel request)
        {
            var viewModel = await _planPaymentFactory.PrepareListModel(request, true);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        #endregion Public Methods
    }
}