using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Companies;
using Advertise.Core.Types;
using Advertise.Service.Companies;
using Advertise.Service.Factory.Companies;
using Advertise.Service.Services.Permissions;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class CompanyVideoController : BaseController
    {
        #region Private Fields

        private readonly ICompanyVideoFactory _companyVideoFactory;
        private readonly ICompanyVideoService _companyVideoService;

        #endregion Private Fields

        #region Public Constructors

        public CompanyVideoController(ICompanyVideoService companyVideoService, ICompanyVideoFactory companyVideoFactory)
        {
            _companyVideoService = companyVideoService;
            _companyVideoFactory = companyVideoFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanCompanyVideoCreate)]
        public virtual async Task<HttpResponseMessage> Create(CompanyVideoCreateModel viewModel)
        {
            await _companyVideoService.CreateByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanCompanyVideoDeleteAjax)]
        public virtual async Task<HttpResponseMessage> DeleteAjax(RequestModel model)
        {
            await _companyVideoService.DeleteByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanCompanyVideoEdit)]
        public virtual async Task<HttpResponseMessage> Edit(RequestModel model)
        {
            var viewModel = await _companyVideoFactory.PrepareEditModelAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanCompanyVideoEditApprove)]
        [HttpPost]
        public virtual async Task<HttpResponseMessage> EditApprove(CompanyVideoEditModel viewModel)
        {
            await _companyVideoService.EditApproveByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanCompanyVideoEditReject)]
        public virtual async Task<HttpResponseMessage> EditReject(CompanyVideoEditModel viewModel)
        {
            await _companyVideoService.EditRejectByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public virtual async Task<HttpResponseMessage> GetFilesAjax(RequestModel model)
        {
            var files = await _companyVideoService.GetFilesAsFineUploaderModelByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, files);
        }

        [MvcAuthorize(PermissionConst.CanCompanyVideoList)]
        public virtual async Task<HttpResponseMessage> List(CompanyVideoSearchModel request)
        {
            var viewModel = await _companyVideoFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanCompanyVideoMyDeleteAjax)]
        public virtual async Task<HttpResponseMessage> MyDeleteAjax(RequestModel model)
        {
            await _companyVideoService.DeleteByIdAsync(model.Id, true);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanCompanyVideoMyEdit)]
        public virtual async Task<HttpResponseMessage> MyEdit(RequestModel model)
        {
            var viewModel = await _companyVideoFactory.PrepareEditModelAsync(model.Id, true);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanCompanyVideoMyEdit)]
        public virtual async Task<HttpResponseMessage> MyEdit(CompanyVideoEditModel viewModel)
        {
            await _companyVideoService.EditByViewModelAsync(viewModel, true);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanCompanyVideoMyList)]
        public virtual async Task<HttpResponseMessage> MyList(CompanyVideoSearchModel request)
        {
            var viewModel = await _companyVideoFactory.PrepareListModelAsync(request, true);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [AllowCrossSite]
        public virtual async Task<HttpResponseMessage> Detail(RequestModel model)
        {
            var viewModel = await _companyVideoFactory.PrepareDetailModelAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        public virtual async Task<HttpResponseMessage> ApproveAjax(RequestModel model)
        {
            await _companyVideoService.SetStateByIdAsync(model.Id, StateType.Approved);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public virtual async Task<HttpResponseMessage> RejectAjax(RequestModel model)
        {
            await _companyVideoService.SetStateByIdAsync(model.Id, StateType.Rejected);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        #endregion Public Methods
    }
}