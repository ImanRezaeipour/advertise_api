using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Products;
using Advertise.Core.Types;
using Advertise.Service.Factory.Products;
using Advertise.Service.Products;
using Advertise.Service.Services.Permissions;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class ProductCommentController : BaseController
    {
        #region Private Fields

        private readonly IProductCommentFactory _productCommentFactory;
        private readonly IProductCommentService _productCommentService;

        #endregion Private Fields

        #region Public Constructors

        public ProductCommentController(IProductCommentService productCommentService, IProductCommentFactory productCommentFactory)
        {
            _productCommentService = productCommentService;
            _productCommentFactory = productCommentFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        [MvcAuthorize(PermissionConst.CanProductCommentCreateAjax)]
        public virtual async Task<HttpResponseMessage> CreateAjax(ProductCommentCreateModel viewModel)
        {
            await _productCommentService.CreateByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanProductCommentDeleteAjax)]
        public virtual async Task<HttpResponseMessage> DeleteAjax(RequestModel model)
        {
            await _productCommentService.DeleteByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public virtual async Task<HttpResponseMessage> Edit(RequestModel model)
        {
            var viewModel = await _productCommentFactory.PrepareEditModelAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanProductCommentEditApprove)]
        public virtual async Task<HttpResponseMessage> EditApprove(ProductCommentEditModel viewModel)
        {
            await _productCommentService.EditApproveByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanProductCommentEditReject)]
        public virtual async Task<HttpResponseMessage> EditReject(ProductCommentEditModel viewModel)
        {
            await _productCommentService.EditRejectByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanProductCommentList)]
        public virtual async Task<HttpResponseMessage> List(ProductCommentSearchModel request)
        {
            var viewModel = await _productCommentFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanProductCommentMyDeleteAjax)]
        public virtual async Task<HttpResponseMessage> MyDeleteAjax(RequestModel model)
        {
            await _productCommentService.DeleteByIdAsync(model.Id, true);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanProductCommentMyEdit)]
        public virtual async Task<HttpResponseMessage> MyEdit(RequestModel model)
        {
            var viewModel = await _productCommentFactory.PrepareEditModelAsync(model.Id, true);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanProductCommentMyEdit)]
        public virtual async Task<HttpResponseMessage> MyEdit(ProductCommentEditModel viewModel)
        {
            await _productCommentService.EditByViewModelAsync(viewModel, true);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanProductCommentMyList)]
        public virtual async Task<HttpResponseMessage> MyList(ProductCommentSearchModel request)
        {
            var viewModel = await _productCommentFactory.PrepareListModelAsync(request, true);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        public virtual async Task<HttpResponseMessage> ApproveAjax(RequestModel model)
        {
            await _productCommentService.SetStateByIdAsync(model.Id, StateType.Approved);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public virtual async Task<HttpResponseMessage> RejectAjax(RequestModel model)
        {
            await _productCommentService.SetStateByIdAsync(model.Id, StateType.Rejected);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        #endregion Public Methods
    }
}