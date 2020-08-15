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
    public class CompanyController : BaseController
    {
        #region Private Fields

        private readonly ICompanyFactory _companyFactory;
        private readonly ICompanyService _companyService;

        #endregion Private Fields

        #region Public Constructors

        public CompanyController(ICompanyService companyService, ICompanyFactory companyFactory)
        {
            _companyService = companyService;
            _companyFactory = companyFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        [AllowCrossSite]
        public virtual async Task<HttpResponseMessage> DetailInfoAjax(RequestModel model)
        {
            var companyDetail = await _companyFactory.PrepareDetailInfoModelAsync(model.Id.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, companyDetail);
        }

        [MvcAuthorize(PermissionConst.CanCompanyEdit)]
        public virtual async Task<HttpResponseMessage> Edit(RequestModel model)
        {
            var viewModel = await _companyFactory.PrepareEditModelAsync(model.Code);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanCompanyEditApprove)]
        [HttpPost]
        public virtual async Task<HttpResponseMessage> EditApprove(CompanyEditModel viewModel)
        {
            await _companyService.EditApproveByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public virtual async Task<HttpResponseMessage> EditReject(CompanyEditModel viewModel)
        {
            await _companyService.EditRejectByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public virtual async Task<HttpResponseMessage> GetAllCompaniesAjax()
        {
            var companies = await _companyService.GetAllNearAsync();
            return Request.CreateResponse(HttpStatusCode.OK, companies);
        }

        public virtual async Task<HttpResponseMessage> GetCountCompanyInCategoryAjax(RequestModel model)
        {
            var count = await _companyService.CountByCategoryIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, count);
        }

        public virtual async Task<HttpResponseMessage> GetFileAjax(RequestModel model)
        {
            var file = await _companyService.GetFileAsFineUploaderModelByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, file);
        }

        public virtual async Task<HttpResponseMessage> GetFileCoverAjax(RequestModel model)
        {
            var fileCover = await _companyService.GetFileCoverAsFineUploaderModelByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, fileCover);
        }

        public virtual async Task<HttpResponseMessage> IsExistAliasAjax(RequestModel model)
        {
            var isExist = await _companyService.IsExistByAliasAsync(model.Code);
            return Request.CreateResponse(HttpStatusCode.OK, isExist);
        }

        [MvcAuthorize(PermissionConst.CanCompanyList)]
        public virtual async Task<HttpResponseMessage> List(CompanySearchModel request)
        {
            var viewModel = await _companyFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanCompanyMyEdit)]
        public virtual async Task<HttpResponseMessage> MyEdit()
        {
            var viewModel = await _companyFactory.PrepareEditModelAsync(applyCurrentUser: true);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanCompanyMyEdit)]
        public virtual async Task<HttpResponseMessage> MyEdit(CompanyEditModel viewModel)
        {
            await _companyService.EditByViewModelAsync(viewModel, true);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public virtual async Task<HttpResponseMessage> Detail(RequestModel model)
        {
            var viewModel = await _companyFactory.PrepareDetailModelAsync(model.Code, model.Slug);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        public virtual async Task<HttpResponseMessage> ApproveAjax(RequestModel model)
        {
            await _companyService.SetStateByIdAsync(model.Id, StateType.Approved);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public virtual async Task<HttpResponseMessage> RejectAjax(RequestModel model)
        {
            await _companyService.SetStateByIdAsync(model.Id, StateType.Rejected);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        
        public virtual async Task<HttpResponseMessage> ProfileMenu()
        {
            var viewModel = await _companyFactory.PrepareProfileMenuModelAsync();
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }
        
        #endregion Public Methods
    }
}