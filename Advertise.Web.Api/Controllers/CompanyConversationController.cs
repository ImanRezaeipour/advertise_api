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
    public class CompanyConversationController : BaseController
    {
        #region Private Fields

        private readonly ICompanyConversationFactory _companyConversationFactory;
        private readonly ICompanyConversationService _companyConversationService;

        #endregion Private Fields

        #region Public Constructors

        public CompanyConversationController(ICompanyConversationService companyConversationService, ICompanyConversationFactory companyConversationFactory)
        {
            _companyConversationService = companyConversationService;
            _companyConversationFactory = companyConversationFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        [MvcAuthorize(PermissionConst.CanCompanyConversationCreateAjax)]
        public virtual async Task<HttpResponseMessage> CreateAjax(CompanyConversationCreateModel viewModel)
        {
            await _companyConversationService.CreateByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanCompanyConversationDeleteAjax)]
        public virtual async Task<HttpResponseMessage> DeleteAjax(RequestModel model)
        {
            await _companyConversationService.DeleteByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public virtual async Task<HttpResponseMessage> DetailAjax(RequestModel model)
        {
            var viewModel = await _companyConversationService.GetListByUserIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }
     
        [MvcAuthorize(PermissionConst.CanCompanyConversationList)]
        public virtual async Task<HttpResponseMessage> List(CompanyConversationSearchModel request)
        {
            var viewModel = await _companyConversationFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }
     
        [MvcAuthorize(PermissionConst.CanCompanyConversationMyList)]
        public virtual async Task<HttpResponseMessage> MyList(CompanyConversationSearchModel request)
        {
            var viewModel = await _companyConversationFactory.PrepareListModelAsync(request ,true);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        #endregion Public Methods
    }
}