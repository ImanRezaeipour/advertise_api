using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Products;
using Advertise.Service.Products;
using Advertise.Service.Services.Permissions;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class ProductFeatureController : BaseController
    {
        #region Private Fields

        private readonly IProductFeatureService _productFeature;

        #endregion Private Fields

        #region Public Constructors

        public ProductFeatureController(IProductFeatureService productFeature)
        {
            _productFeature = productFeature;
        }

        #endregion Public Constructors

        #region Public Methods

        [MvcAuthorize(PermissionConst.CanProductFeatureDeleteAjax)]
        public virtual async Task<HttpResponseMessage> DeleteAjax(RequestModel model)
        {
            await _productFeature.DeleteByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanProductFeatureGetListAjax)]
        public virtual async Task<HttpResponseMessage> GetListAjax()
        {
            var request = new ProductFeatureSearchModel();
            var productFeatures = await _productFeature.ListByRequestAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, productFeatures);
        }

        #endregion Public Methods
    }
}