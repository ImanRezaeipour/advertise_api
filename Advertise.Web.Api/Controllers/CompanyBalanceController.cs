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
    public class CompanyBalanceController : BaseController
    {
        #region Private Fields

        private readonly ICompanyBalanceFactory _companyBalanceFactory;
        private readonly ICompanyBalanceService _companyBalanceService;

        #endregion Private Fields

        #region Public Constructors
     
        public CompanyBalanceController(ICompanyBalanceService companyBalanceService, ICompanyBalanceFactory companyBalanceFactory)
        {
            _companyBalanceService = companyBalanceService;
            _companyBalanceFactory = companyBalanceFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        [MvcAuthorize(PermissionConst.CanCompanyBalanceCreate)]
        public virtual async Task<HttpResponseMessage> Create()
        {
            var viewModel = await _companyBalanceFactory.PrepareCreateModelAsync();
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanCompanyBalanceCreate)]
        [HttpPost]
        public virtual async Task<HttpResponseMessage> Create(CompanyBalanceCreateModel viewModel)
        {
            await _companyBalanceService.CreateByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanCompanyBalanceDeleteAjax)]
        public virtual async Task<HttpResponseMessage> DeleteAjax(RequestModel model)
        {
            await _companyBalanceService.DeleteByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanCompanyBalanceEdit)]
        public virtual async Task<HttpResponseMessage> Edit(RequestModel model)
        {
            var viewModel = await _companyBalanceFactory.PrepareEditModelAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanCompanyBalanceEdit)]
        public virtual async Task<HttpResponseMessage> Edit(CompanyBalanceEditModel viewModel)
        {
            await _companyBalanceService.EditByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanCompanyBalanceGetFileAjax)]
        public virtual async Task<HttpResponseMessage> GetFileAjax(RequestModel model)
        {
            var file = await _companyBalanceService.GetFileAsFineUploaderModelByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, file);
        }

        [MvcAuthorize(PermissionConst.CanCompanyBalanceList)]
        public virtual async Task<HttpResponseMessage> List(CompanyBalanceSearchModel request)
        {
            var viewModel = await _companyBalanceFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }
     
        [MvcAuthorize(PermissionConst.CanCompanyBalanceMyList)]
        public virtual async Task<HttpResponseMessage> MyList(CompanyBalanceSearchModel request)
        {
            var viewModel = await _companyBalanceFactory.PrepareListModelAsync(request, true);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        #endregion Public Methods
    }
}