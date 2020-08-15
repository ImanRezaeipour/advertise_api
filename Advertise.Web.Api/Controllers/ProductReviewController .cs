using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Products;
using Advertise.Service.Factory.Products;
using Advertise.Service.Products;
using Advertise.Service.Services.Permissions;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class ProductReviewController : BaseController
    {
        #region Private Fields

        private readonly IProductReviewService _productReviewService;
        private readonly IProductReviewFactory _productReviewFactory;

        #endregion Private Fields

        #region Public Constructors

        public ProductReviewController(IProductReviewService productReviewService, IProductReviewFactory productReviewFactory)
        {
            _productReviewService = productReviewService;
            _productReviewFactory = productReviewFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        [MvcAuthorize(PermissionConst.CanProductReviewCreate)]
        [HttpPost]
        public virtual async Task<HttpResponseMessage> Create(ProductReviewCreateModel viewModel)
        {
            await _productReviewService.CreateByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanProductReviewCreate)]
        public virtual async Task<HttpResponseMessage> Create()
        {
            var viewModel = await _productReviewFactory.PrepareCreateModelAsync();
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanProductReviewDeleteAjax)]
        public virtual async Task<HttpResponseMessage> DeleteAjax(RequestModel model)
        {
            await _productReviewService.DeleteByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanProductReviewEdit)]
        public virtual async Task<HttpResponseMessage> Edit(RequestModel model)
        {
            var viewModel = await _productReviewFactory.PrepareEditModelAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanProductReviewEdit)]
        [HttpPost]
        public virtual async Task<HttpResponseMessage> Edit(ProductReviewEditModel viewModel)
        {
            await _productReviewService.EditByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanProductReviewList)]
        public virtual async Task<HttpResponseMessage> List(ProductReviewSearchModel request)
        {
            var viewModel = await _productReviewFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        #endregion Public Methods
    }
}