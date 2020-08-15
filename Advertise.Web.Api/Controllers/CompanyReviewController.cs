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
    public class CompanyReviewController : BaseController
    {
        #region Private Fields

        private readonly ICompanyReviewFactory _companyReviewFactory;
        private readonly ICompanyReviewService _companyReviewService;

        #endregion Private Fields

        #region Public Constructors

        public CompanyReviewController(ICompanyReviewService companyReviewService, ICompanyReviewFactory companyReviewFactory)
        {
            _companyReviewService = companyReviewService;
            _companyReviewFactory = companyReviewFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        [MvcAuthorize(PermissionConst.CanCompanyReviewCreate)]
        [HttpPost]
        public virtual async Task<HttpResponseMessage> Create(CompanyReviewCreateModel viewModel)
        {
            await _companyReviewService.CreateByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanCompanyReviewCreate)]
        public virtual async Task<HttpResponseMessage> Create()
        {
            var viewModel = await _companyReviewFactory.PrepareCreateModelAsync();
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanCompanyReviewDeleteAjax)]
        public virtual async Task<HttpResponseMessage> DeleteAjax(RequestModel model)
        {
            await _companyReviewService.DeleteByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanCompanyReviewEdit)]
        public virtual async Task<HttpResponseMessage> Edit(RequestModel model)
        {
            var viewModel = await _companyReviewFactory.PrepareEditViewModelAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanCompanyReviewEdit)]
        [HttpPost]
        public virtual async Task<HttpResponseMessage> Edit(CompanyReviewEditModel viewModel)
        {
            await _companyReviewService.EditByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanCompanyReviewList)]
        public virtual async Task<HttpResponseMessage> List(CompanyReviewSearchModel request)
        {
            var viewModel = await _companyReviewFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        #endregion Public Methods
    }
}