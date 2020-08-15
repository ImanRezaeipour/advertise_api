using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Companies;
using Advertise.Service.Companies;
using Advertise.Service.Factory.Companies;
using Advertise.Service.Services.Permissions;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class CompanyFollowController : BaseController
    {
        #region Private Fields

        private readonly ICompanyFollowFactory _companyFollowFactory;
        private readonly ICompanyFollowService _companyFollowService;

        #endregion Private Fields

        #region Public Constructors

        public CompanyFollowController(ICompanyFollowService companyFollowService, ICompanyFollowFactory companyFollowFactory)
        {
            _companyFollowService = companyFollowService;
            _companyFollowFactory = companyFollowFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual async Task<HttpResponseMessage> CountAjax(RequestModel model)
        {
            var count = await _companyFollowService.CountAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, count);
        }

        public virtual async Task<HttpResponseMessage> MyFollowerList()
        {
            var viewModel = await _companyFollowFactory.PrepareListModelAsync(true, true);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanCompanyFollowMyFollowList)]
        public virtual async Task<HttpResponseMessage> MyFollowList()
        {
            var viewModel = await _companyFollowFactory.PrepareListModelAsync(true);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanCompanyFollowMyFollowList)]
        public virtual async Task<HttpResponseMessage> MyFollowListMoreAjax(CompanyFollowSearchModel request)
        {
            var viewModel = await _companyFollowService.ListByRequestAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanCompanyFollowMyIsFollowAjax)]
        public virtual async Task<HttpResponseMessage> MyIsFollowAjax(RequestModel model)
        {
            var isFollow = await _companyFollowService.IsFollowByCurrentUserAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, isFollow);
        }

        [MvcAuthorize(PermissionConst.CanCompanyFollowMyToggleFollowAjax)]
        public virtual async Task<HttpResponseMessage> MyToggleAjax(RequestModel model)
        {
            await _companyFollowService.ToggleFollowCurrentUserByCompanyIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        #endregion Public Methods
    }
}