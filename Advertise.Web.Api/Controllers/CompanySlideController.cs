using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Companies;
using Advertise.Service.Companies;
using Advertise.Service.Factory.Companies;

namespace Advertise.Web.Api.Controllers
{
    public class CompanySlideController : BaseController
    {
        #region Private Fields

        private readonly ICompanySlideFactory _companySlideFactory;
        private readonly ICompanySlideService _companySlideService;

        #endregion Private Fields

        #region Public Constructors

        public CompanySlideController(ICompanySlideService companySlideService, ICompanySlideFactory companySlideFactory)
        {
            _companySlideService = companySlideService;
            _companySlideFactory = companySlideFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual async Task<HttpResponseMessage> Create()
        {
            var viewModel = await _companySlideFactory.PrepareCreateModelAsync();
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        public virtual async Task<HttpResponseMessage> Create(CompanySlideCreateModel viewModel)
        {
            await _companySlideService.CreateByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public virtual async Task<HttpResponseMessage> DeleteAjax(RequestModel model)
        {
            await _companySlideService.DeleteByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public virtual async Task<HttpResponseMessage> Edit(RequestModel model)
        {
            var viewModel = await _companySlideFactory.PrepareEditModelAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        public virtual async Task<HttpResponseMessage> Edit(CompanySlideEditModel viewModel)
        {
            await _companySlideService.EditByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public virtual async Task<HttpResponseMessage> GetFileAjax(RequestModel model)
        {
            var files = await _companySlideService.GetFileAsFineUploaderModelByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, files);
        }

        public virtual async Task<HttpResponseMessage> List(CompanySlideSearchModel request)
        {
            var viewModel = await _companySlideFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        public virtual async Task<HttpResponseMessage> BulkEdit()
        {
            var viewModel = await _companySlideFactory.PrepareBulkEditModelAsync();
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        #endregion Public Methods
    }
}