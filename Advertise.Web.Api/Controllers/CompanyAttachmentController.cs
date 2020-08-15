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
    public class CompanyAttachmentController : BaseController
    {
        #region Private Fields

        private readonly ICompanyAttachmentFactory _companyAttachmentFactory;
        private readonly ICompanyAttachmentService _companyAttachmentService;

        #endregion Private Fields

        #region Public Constructors

        public CompanyAttachmentController(ICompanyAttachmentService companyAttachmentService, ICompanyAttachmentFactory companyAttachmentFactory)
        {
            _companyAttachmentService = companyAttachmentService;
            _companyAttachmentFactory = companyAttachmentFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        [MvcAuthorize(PermissionConst.CanCompanyAttachmentCreate)]
        [HttpPost]
        public virtual async Task<HttpResponseMessage> Create(CompanyAttachmentCreateModel viewModel)
        {
            await _companyAttachmentService.CreateByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanCompanyAttachmentDeleteAjax)]
        public virtual async Task<HttpResponseMessage> DeleteAjax(RequestModel model)
        {
            await _companyAttachmentService.DeleteByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanCompanyAttachmentEdit)]
        public virtual async Task<HttpResponseMessage> Edit(RequestModel model)
        {
            var viewModel = await _companyAttachmentFactory.PrepareEditModelAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanCompanyAttachmentEditApprove)]
        public virtual async Task<HttpResponseMessage> EditApprove(CompanyAttachmentEditModel viewModel)
        {
            await _companyAttachmentService.EditApproveByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public virtual async Task<HttpResponseMessage> EditReject(CompanyAttachmentEditModel viewModel)
        {
            await _companyAttachmentService.EditRejectByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public virtual async Task<HttpResponseMessage> GetFilesAjax(RequestModel model)
        {
            var files = await _companyAttachmentService.GetFilesAsFineUploaderModelByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, files);
        }

        [MvcAuthorize(PermissionConst.CanCompanyAttachmentList)]
        public virtual async Task<HttpResponseMessage> List(CompanyAttachmentSearchModel request)
        {
            var viewModel = await _companyAttachmentFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanCompanyAttachmentMyDeleteAjax)]
        public virtual async Task<HttpResponseMessage> MyDeleteAjax(RequestModel model)
        {
            await _companyAttachmentService.DeleteByIdAsync(model.Id, true);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanCompanyAttachmentMyEdit)]
        public virtual async Task<HttpResponseMessage> MyEdit(RequestModel model)
        {
            var viewModel = await _companyAttachmentFactory.PrepareEditModelAsync(model.Id, true);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanCompanyAttachmentMyEdit)]
        public virtual async Task<HttpResponseMessage> MyEdit(CompanyAttachmentEditModel viewModel)
        {
            await _companyAttachmentService.EditByViewModelAsync(viewModel, true);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanCompanyAttachmentMyList)]
        public virtual async Task<HttpResponseMessage> MyList(CompanyAttachmentSearchModel request)
        {
            var viewModel = await _companyAttachmentFactory.PrepareListModelAsync(request, true);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [AllowCrossSite]
        public virtual async Task<HttpResponseMessage> Detail(RequestModel model)
        {
            var viewModel = await _companyAttachmentFactory.PrepareDetailModelAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        public virtual async Task<HttpResponseMessage> ApproveAjax(RequestModel model)
        {
            await _companyAttachmentService.SetStateByIdAsync(model.Id, StateType.Approved);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public virtual async Task<HttpResponseMessage> RejectAjax(RequestModel model)
        {
            await _companyAttachmentService.SetStateByIdAsync(model.Id, StateType.Rejected);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        #endregion Public Methods
    }
}