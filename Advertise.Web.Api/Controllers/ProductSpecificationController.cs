using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Advertise.Core.Model.Common;
using Advertise.Service.Factory.Products;
using Advertise.Service.Products;
using Advertise.Service.Services.Permissions;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class ProductSpecificationController : BaseController
    {
        #region Private Fields

        private readonly IProductSpecificationFactory _productSpecificationFactory;
        private readonly IProductSpecificationService _productSpecificationService;

        #endregion Private Fields

        #region Public Constructors

        public ProductSpecificationController(IProductSpecificationService productSpecificationService, IProductSpecificationFactory productSpecificationFactory)
        {
            _productSpecificationService = productSpecificationService;
            _productSpecificationFactory = productSpecificationFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual async Task<HttpResponseMessage> CreateAjax(RequestModel model)
        {
            var viewModel = await _productSpecificationFactory.PrepareCreateModelAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanProductSpecificationDeleteAjax)]
        public virtual async Task<HttpResponseMessage> DeleteAjax(RequestModel model)
        {
            await _productSpecificationService.DeleteByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanProductSpecificationEditAjax)]
        public virtual async Task<HttpResponseMessage> EditAjax(RequestModel model)
        {
            var viewModel = await _productSpecificationFactory.PrepareEditModelAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        #endregion Public Methods
    }
}