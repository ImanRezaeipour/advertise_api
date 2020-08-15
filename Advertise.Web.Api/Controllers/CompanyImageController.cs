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
    public class CompanyImageController : BaseController
    {
        #region Private Fields

        private readonly ICompanyImageFactory _companyImageFactory;
        private readonly ICompanyImageService _companyImageService;

        #endregion Private Fields

        #region Public Constructors

        public CompanyImageController(ICompanyImageService companyImageService, ICompanyImageFactory companyImageFactory)
        {
            _companyImageService = companyImageService;
            _companyImageFactory = companyImageFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        [MvcAuthorize(PermissionConst.CanCompanyImageCreate)]
        [HttpPost]
        public virtual async Task<HttpResponseMessage> Create(CompanyImageCreateModel viewModel)
        {
            await _companyImageService.CreateByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanCompanyImageDeleteAjax)]
        public virtual async Task<HttpResponseMessage> DeleteAjax(RequestModel model)
        {
            await _companyImageService.DeleteByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanCompanyImageEdit)]
        public virtual async Task<HttpResponseMessage> Edit(RequestModel model)
        {
            var viewModel = await _companyImageFactory.PrepareEditModelAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanCompanyImageEditApprove)]
        public virtual async Task<HttpResponseMessage> EditApprove(CompanyImageEditModel viewModel)
        {
            await _companyImageService.EditApproveByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public virtual async Task<HttpResponseMessage> EditReject(CompanyImageEditModel viewModel)
        {
            await _companyImageService.EditRejectByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        public virtual async Task<HttpResponseMessage> GetFilesAjax(RequestModel model)
        {
            var files = await _companyImageService.GetFilesAsFineUploaderModelByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, files);
        }

        [MvcAuthorize(PermissionConst.CanCompanyImageList)]
        public virtual async Task<HttpResponseMessage> List(CompanyImageSearchModel request)
        {
            var viewModel = await _companyImageFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanCompanyImageMyDeleteAjax)]
        public virtual async Task<HttpResponseMessage> MyDeleteAjax(RequestModel model)
        {
            await _companyImageService.DeleteByIdAsync(model.Id, true);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanCompanyImageMyEdit)]
        public virtual async Task<HttpResponseMessage> MyEdit(RequestModel model)
        {
            var viewModel = await _companyImageFactory.PrepareEditModelAsync(model.Id, true);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanCompanyImageMyEdit)]
        public virtual async Task<HttpResponseMessage> MyEdit(CompanyImageEditModel viewModel)
        {
            await _companyImageService.EditByViewModelAsync(viewModel, true);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanCompanyImageMyList)]
        public virtual async Task<HttpResponseMessage> MyList(CompanyImageSearchModel request)
        {
            var viewModel = await _companyImageFactory.PrepareListModelAsync(request, true);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        public virtual async Task<HttpResponseMessage> ApproveAjax(RequestModel model)
        {
            await _companyImageService.SetStateByIdAsync(model.Id, StateType.Approved);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public virtual async Task<HttpResponseMessage> RejectAjax(RequestModel model)
        {
            await _companyImageService.SetStateByIdAsync(model.Id, StateType.Rejected);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        #endregion Public Methods
    }
}