using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Advertise.Core.Model.Carts;
using Advertise.Core.Model.Common;
using Advertise.Service.Carts;
using Advertise.Service.Factory.Carts;
using Advertise.Service.Services.Permissions;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class CartController : BaseController
    {
        #region Private Fields

        private readonly ICartFactory _cartFactory;
        private readonly ICartService _cartService;

        #endregion Private Fields

        #region Public Constructors

        public CartController(ICartFactory cartFactory, ICartService cartService)
        {
            _cartFactory = cartFactory;
            _cartService = cartService;
        }

        #endregion Public Constructors

        #region Public Methods
     
        [HttpPost]
        [MvcAuthorize(PermissionConst.CanBagChangeProductCountAjax)]
        public virtual async Task<HttpResponseMessage> ChangeProductCountAjax(RequestModel model)
        {
            await _cartService.SetProductCountByIdAsync(model.Id, model.Count);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
      
        [MvcAuthorize(PermissionConst.CanBagCountAjax)]
        public virtual async Task<HttpResponseMessage> CountAjax()
        {
            var count = await _cartService.CountByCurrentUserAsync();
            return Request.CreateResponse(HttpStatusCode.OK, count);
        }

        [MvcAuthorize(PermissionConst.CanBagCreateAjax)]
        public virtual async Task<HttpResponseMessage> CreateAjax(RequestModel model)
        {
            await _cartService.CreateByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    
        [HttpPost]
        [MvcAuthorize(PermissionConst.CanBagDeleteAjax)]
        public virtual async Task<HttpResponseMessage> DeleteAjax(RequestModel model)
        {
            await _cartService.DeleteByProductIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public virtual async Task<HttpResponseMessage> Edit()
        {
            var viewModel = await _cartFactory.PrepareInfoModelAsync();
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }
    
        [MvcAuthorize(PermissionConst.CanBagIsExistAjax)]
        public virtual async Task<HttpResponseMessage> IsExistAjax(RequestModel model)
        {
            var isExist = await _cartService.IsExistByProductIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, isExist);
        }

        [MvcAuthorize(PermissionConst.CanBagList)]
        public virtual async Task<HttpResponseMessage> List(CartSearchModel request)
        {
            var viewModel = await _cartFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanBagList)]
        public virtual async Task<HttpResponseMessage> ListMoreAjax(CartSearchModel request)
        {
            var viewModel = await _cartFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanBagMyList)]
        public virtual async Task<HttpResponseMessage> MyList(CartSearchModel request)
        {
            var viewModel = await _cartFactory.PrepareListModelAsync(request, true);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanBagMyList)]
        public virtual async Task<HttpResponseMessage> MyListMoreAjax(CartSearchModel request)
        {
            var viewModel = await _cartFactory.PrepareListModelAsync(request, true);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanBagToggleAjax)]
        public virtual async Task<HttpResponseMessage> ToggleAjax(RequestModel model)
        {
            await _cartService.ToggleByCurrentUserAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        #endregion Public Methods
    }
}