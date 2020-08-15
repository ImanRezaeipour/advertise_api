using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Advertise.Core.Model.Companies;
using Advertise.Service.Companies;
using Advertise.Service.Factory.Companies;

namespace Advertise.Web.Api.Controllers
{
    public class CompanyHourController : BaseController
    {
        #region Private Fields

        private readonly ICompanyHourFactory _companyHourFactory;
        private readonly ICompanyHourService _companyHourService;

        #endregion Private Fields

        #region Public Constructors

        public CompanyHourController(ICompanyHourFactory companyHourFactory, ICompanyHourService companyHourService)
        {
            _companyHourFactory = companyHourFactory;
            _companyHourService = companyHourService;
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual async Task<HttpResponseMessage> Edit()
        {
            var viewModel = await _companyHourFactory.PrepareEditModelAsync();
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        public virtual async Task<HttpResponseMessage> Edit(CompanyHourEditModel viewModel)
        {
            await _companyHourService.EditByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        #endregion Public Methods
    }
}