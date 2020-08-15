using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Advertise.Core.Model.Common;
using Advertise.Service.Products;
using Advertise.Service.Services.Permissions;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class ProductImageController : BaseController
    {
        #region Private Fields

        private readonly IProductImageService _productImageService;

        #endregion Private Fields

        #region Public Constructors

        public ProductImageController(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        #endregion Public Constructors

        #region Public Methods

        [MvcAuthorize(PermissionConst.CanProductImageDeleteAjax)]
        public virtual async Task<HttpResponseMessage> DeleteAjax(RequestModel model)
        {
            await _productImageService.DeleteByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanProductImageGetListAjax)]
        public virtual async Task<HttpResponseMessage> GetListAjax(RequestModel model)
        {
            var productImages = _productImageService.GetByProductIdAsFileModelAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, productImages);
        }
    }

    #endregion Public Methods
}