using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Receipts;
using Advertise.Service.Factory.Receipts;
using Advertise.Service.Receipts;
using Advertise.Service.Services.Permissions;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class ReceiptController : BaseController
    {
        #region Private Fields

        private readonly IReceiptFactory _receiptFactory;
        private readonly IReceiptService _receiptService;

        #endregion Private Fields

        #region Public Constructors


        public ReceiptController(IReceiptService receiptService,IReceiptFactory receiptFactory)
        {
            _receiptService = receiptService;
            _receiptFactory = receiptFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual async Task<HttpResponseMessage> CheckOut()
        {
            var viewModel = await _receiptFactory.PrepareCreateModelAsync();
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        public virtual async Task<HttpResponseMessage> CheckOut(ReceiptModel viewModel)
        {
            await _receiptService.FinalUpdateByViewModel(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanReceiptList)]
        public virtual async Task<HttpResponseMessage> List(ReceiptSearchModel request)
        {
            var viewModel = await _receiptFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanReceiptMyList)]
        public virtual async Task<HttpResponseMessage> MyList(ReceiptSearchModel request)
        {
            var viewModel = await _receiptFactory.PrepareListModelAsync(request, true);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        public virtual async Task<HttpResponseMessage> Preview(RequestModel model)
        {
            var viewModel = await _receiptFactory.PreparePreviewModelAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        #endregion Public Methods
    }
}