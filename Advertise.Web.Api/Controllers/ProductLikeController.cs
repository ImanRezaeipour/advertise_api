using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Products;
using Advertise.Service.Factory.Products;
using Advertise.Service.Products;
using Advertise.Service.Services.Permissions;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class ProductLikeController : BaseController
    {
        #region Private Fields

        private readonly IProductLikeService _productLikeService;
        private readonly IProductLikeFactory _productLikeFactory;

        #endregion Private Fields

        #region Public Constructors

        public ProductLikeController(IProductLikeService productLikeService, IProductLikeFactory productLikeFactory)
        {
            _productLikeService = productLikeService;
            _productLikeFactory = productLikeFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual async Task<HttpResponseMessage> CountAjax(RequestModel model)
        {
            var count = await _productLikeService.CountByProductIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, count);
        }

        [MvcAuthorize(PermissionConst.CanProductLikeMyLikeList)]
        public virtual async Task<HttpResponseMessage> MyLikeList(ProductLikeSearchModel request)
        {
            var viewModel = await _productLikeFactory.PrepareListModelAsync(request, true);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanProductLikeMyLikerList)]
        public virtual async Task<HttpResponseMessage> MyLikerList(ProductLikeSearchModel request)
        {
            var viewModel = await _productLikeFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanProductLikeMyToggleAjax)]
        public virtual async Task<HttpResponseMessage> MyToggleAjax(RequestModel model)
        {
            await _productLikeService.ToggleCurrentUserByProductIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        #endregion Public Methods
    }
}