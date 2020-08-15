using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Seos;
using Advertise.Service.Factory.Seos;
using Advertise.Service.Seos;
using Advertise.Service.Services.Permissions;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class SeoUrlController : BaseController
    {
        #region Private Fields

        private readonly ISeoUrlFactory _seoUrlFactory;
        private readonly ISeoUrlService _seoUrlService;

        #endregion Private Fields

        #region Public Constructors

        public SeoUrlController(ISeoUrlService seoUrlService, ISeoUrlFactory seoUrlFactory)
        {
            _seoUrlService = seoUrlService;
            _seoUrlFactory = seoUrlFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        [MvcAuthorize(PermissionConst.CanSeoUrlCreate)]
        public virtual async Task<HttpResponseMessage> Create()
        {
            var viewModel = await _seoUrlFactory.PrepareCreateModelAsync();
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanSeoUrlCreate)]
        public virtual async Task<HttpResponseMessage> Create(SeoUrlCreateModel viewModel)
        {
            await _seoUrlService.CreateByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanSeoUrlDeleteAjax)]
        public virtual async Task<HttpResponseMessage> DeleteAjax(RequestModel model)
        {
            await _seoUrlService.DeleteByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanSeoUrlEdit)]
        public virtual async Task<HttpResponseMessage> Edit(RequestModel model)
        {
            var viewModel = await _seoUrlFactory.PrepareEditModelAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanSeoUrlEdit)]
        public virtual async Task<HttpResponseMessage> Edit(SeoUrlEditModel viewModel)
        {
            await _seoUrlService.EditByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanSeoUrlList)]
        public virtual async Task<HttpResponseMessage> List(SeoUrlSearchModel request)
        {
            var viewModel = await _seoUrlFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        #endregion Public Methods
    }
}