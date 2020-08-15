using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Advertise.Core.Model.Common;
using Advertise.Service.Products;
using Advertise.Service.Services.Permissions;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class ProductNotifyController : BaseController
    {
        #region Private Fields

        private readonly IProductNotifyService _productNotifyService;

        #endregion Private Fields

        #region Public Constructors

        public ProductNotifyController(IProductNotifyService productNotifyService)
        {
            _productNotifyService = productNotifyService;
        }

        #endregion Public Constructors

        #region Public Methods

        [MvcAuthorize(PermissionConst.CanProductNotifyMyToggleAjax)]
        public virtual async Task<HttpResponseMessage> MyToggleAjax(RequestModel model)
        {
            await _productNotifyService.ToggleByProductIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        #endregion Public Methods
    }
}