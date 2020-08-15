using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Advertise.Core.Model.Products;
using Advertise.Service.Products;

namespace Advertise.Web.Api.Controllers
{
    public class ProductRateController : BaseController
    {
        #region Private Fields

        private readonly IProductRateService _productRateService;

        #endregion Private Fields

        #region Public Constructors

        public ProductRateController(IProductRateService productRateService)
        {
            _productRateService = productRateService;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPost]
        public virtual async Task<HttpResponseMessage> CreateAjax(ProductRateCreateModel viewModel)
        {
            await _productRateService.CreateByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        #endregion Public Methods
    }
}