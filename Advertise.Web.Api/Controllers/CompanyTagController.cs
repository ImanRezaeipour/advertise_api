using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Advertise.Core.Model.Common;
using Advertise.Service.Companies;

namespace Advertise.Web.Api.Controllers
{
    public class CompanyTagController : BaseController
    {
        #region Private Fields

        private readonly ICompanyTagService _companyTagService;

        #endregion Private Fields

        #region Public Constructors

        public CompanyTagController(ICompanyTagService companyTagService)
        {
            _companyTagService = companyTagService;
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual async Task<HttpResponseMessage> GetTagsAjax(RequestModel model)
        {
            var tags = await _companyTagService.GetByCompanyIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, tags);
        }

        #endregion Public Methods
    }
}