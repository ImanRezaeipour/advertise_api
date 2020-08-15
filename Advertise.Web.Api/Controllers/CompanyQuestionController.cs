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
    public class CompanyQuestionController : BaseController
    {
        #region Private Fields

        private readonly ICompanyQuestionFactory _companyQuestionFactory;
        private readonly ICompanyQuestionService _companyQuestionService;

        #endregion Private Fields

        #region Public Constructors
      
        public CompanyQuestionController(ICompanyQuestionService companyQuestionService, ICompanyQuestionFactory companyQuestionFactory)
        {
            _companyQuestionService = companyQuestionService;
            _companyQuestionFactory = companyQuestionFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        [MvcAuthorize(PermissionConst.CanCompanyQustionCreate)]
        [HttpPost]
        public virtual async Task<HttpResponseMessage> CreateAjax(CompanyQuestionCreateModel viewModel)
        {
            await _companyQuestionService.CreateByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanCompanyQustionDeleteAjax)]
        public virtual async Task<HttpResponseMessage> DeleteAjax(RequestModel model)
        {
            await _companyQuestionService.DeleteByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanCompanyQustionEdit)]
        public virtual async Task<HttpResponseMessage> Edit(RequestModel model)
        {
            var viewModel = await _companyQuestionFactory.PrepareEditViewModelAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanCompanyQustionEdit)]
        public virtual async Task<HttpResponseMessage> Edit(CompanyQuestionEditModel viewModel)
        {
            await _companyQuestionService.EditByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanCompanyQustionList)]
        public virtual async Task<HttpResponseMessage> List(CompanyQuestionSearchModel request)
        {
            var viewModel = await _companyQuestionFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanCompanyQustionEditApprove)]
        public virtual async Task<HttpResponseMessage> EditApprove(CompanyQuestionEditModel viewModel)
        {
            await _companyQuestionService.ApproveByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanCompanyQustionEditReject)]
        public virtual async Task<HttpResponseMessage> EditReject(CompanyQuestionEditModel viewModel)
        {
            await _companyQuestionService.RejectByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanCompanyQustionMyList)]
        public virtual async Task<HttpResponseMessage> MyList(CompanyQuestionSearchModel request)
        {
            var viewModel = await _companyQuestionFactory.PrepareListModelAsync(request, true);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        #endregion Public Methods
    }
}