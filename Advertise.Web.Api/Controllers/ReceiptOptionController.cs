using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Advertise.Core.Model.Receipts;
using Advertise.Core.Types;
using Advertise.Service.Factory.Receipts;
using Advertise.Service.Services.Permissions;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class ReceiptOptionController : BaseController
    {
        #region Private Fields

        private readonly IReceiptOptionFactory _receiptOptionFactory;

        #endregion Private Fields

        #region Public Constructors

        public ReceiptOptionController(IReceiptOptionFactory receiptOptionFactory)
        {
            _receiptOptionFactory = receiptOptionFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        [MvcAuthorize(PermissionConst.CanReceiptOptionList)]
        public virtual async Task<HttpResponseMessage> List(ReceiptOptionSearchModel request)
        {
            var viewModel = await _receiptOptionFactory.PrepareListModel(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanReceiptOptionMyBuyList)]
        public virtual async Task<HttpResponseMessage> MyBuyList(ReceiptOptionSearchModel request)
        {
            request.ListType = ReceiptOptionListType.Buy;
            var viewModel = await _receiptOptionFactory.PrepareListModel(request, true);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanReceiptOptionMySaleList)]
        public virtual async Task<HttpResponseMessage> MySaleList(ReceiptOptionSearchModel request)
        {
            request.ListType = ReceiptOptionListType.Sale;
            var viewModel = await _receiptOptionFactory.PrepareListModel(request, true);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        #endregion Public Methods
    }
}