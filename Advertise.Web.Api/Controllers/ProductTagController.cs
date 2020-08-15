using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Advertise.Core.Model.Common;
using Advertise.Service.Products;
using Advertise.Service.Services.Permissions;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class ProductTagController : BaseController
    {
        #region Private Fields

        private readonly IProductTagService _productTagService;

        #endregion Private Fields

        #region Public Constructors

        public ProductTagController(IProductTagService productTagService)
        {
            _productTagService = productTagService;
        }

        #endregion Public Constructors

        #region Public Methods

        [MvcAuthorize(PermissionConst.CanTagList)]
        public virtual async Task<HttpResponseMessage> GetTagsAjax(RequestModel model)
        {
            var tags = await _productTagService.GetTagsByProductIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, tags);
        }

        #endregion Public Methods
    }
}