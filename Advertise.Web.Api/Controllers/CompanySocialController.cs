using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Companies;
using Advertise.Service.Companies;
using Advertise.Service.Factory.Companies;
using Advertise.Service.Services.Permissions;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class CompanySocialController : BaseController
    {
        #region Private Fields

        private readonly ICompanySocialFactory _companySocialFactory;
        private readonly ICompanySocialService _companySocialService;

        #endregion Private Fields

        #region Public Constructors

        public CompanySocialController(ICompanySocialService companySocialService, ICompanySocialFactory companySocialFactory)
        {
            _companySocialService = companySocialService;
            _companySocialFactory = companySocialFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        [MvcAuthorize(PermissionConst.CanCompanySocialCreate)]
        [HttpPost]
        public virtual async Task<HttpResponseMessage> Create(CompanySocialCreateModel viewModel)
        {
            await _companySocialService.CreateByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanCompanySocialDeleteAjax)]
        public virtual async Task<HttpResponseMessage> DeleteAjax(RequestModel model)
        {
            await _companySocialService.DeleteByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanCompanySocialEdit)]
        public virtual async Task<HttpResponseMessage> Edit(RequestModel model)
        {
            var viewModel = await _companySocialFactory.PrepareEditModelAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanCompanySocialEdit)]
        public virtual async Task<HttpResponseMessage> Edit(CompanySocialEditModel viewModel)
        {
            await _companySocialService.EditByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanCompanySocialList)]
        public virtual async Task<HttpResponseMessage> List(CompanySocialSearchModel request)
        {
            var viewModel = await _companySocialFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanCompanySocialMyEdit)]
        public virtual async Task<HttpResponseMessage> MyEdit()
        {
            var viewModel = await _companySocialFactory.PrepareEditModelAsync(null, true);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanCompanySocialMyEdit)]
        public virtual async Task<HttpResponseMessage> MyEdit(CompanySocialEditModel viewModel)
        {
            await _companySocialService.EditByViewModelAsync(viewModel, true);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        #endregion Public Methods
    }
}