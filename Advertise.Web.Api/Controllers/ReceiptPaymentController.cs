using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Advertise.Core.Model.Receipts;
using Advertise.Service.Factory.Receipts;
using Advertise.Service.Receipts;
using Advertise.Service.Services.Permissions;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class ReceiptPaymentController : BaseController
    {
        #region Private Fields

        private readonly IReceiptPaymentService _receiptPaymentService;
        private readonly IReceiptPaymentFactory _receiptPaymentFactory;

        #endregion Private Fields

        #region Public Constructors

        public ReceiptPaymentController(IReceiptPaymentService receiptPaymentService, IReceiptPaymentFactory receiptPaymentFactory)
        {
            _receiptPaymentService = receiptPaymentService;
            _receiptPaymentFactory = receiptPaymentFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual async Task<HttpResponseMessage> Callback(ReceiptPaymentCallbackModel viewModel)
        {
            await _receiptPaymentService.CallbackByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public virtual async Task<HttpResponseMessage> Complete(string authority)
        {
            var viewModel = await _receiptPaymentFactory.PrepareCompleteModelAsync(authority);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanReceiptPaymentPay)]
        public virtual async Task<HttpResponseMessage> Create()
        {
            var result = await _receiptPaymentService.CreateAsync();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [MvcAuthorize(PermissionConst.CanReceiptPaymentList)]
        public virtual async Task<HttpResponseMessage> List(ReceiptPaymentSearchModel request)
        {
            var viewModel = await _receiptPaymentFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanReceiptPaymentMyList)]
        public virtual async Task<HttpResponseMessage> MyList(ReceiptPaymentSearchModel request)
        {
            var viewModel = await _receiptPaymentFactory.PrepareListModelAsync(request, true);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        #endregion Public Methods
    }
}