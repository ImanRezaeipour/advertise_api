using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Guarantees;
using Advertise.Service.Factory.Guarantees;
using Advertise.Service.Guarantees;
using Advertise.Service.Services.Permissions;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class GuaranteeController : BaseController
    {
        #region Private Fields

        private readonly IGuaranteeFactory _guaranteeFactory;
        private readonly IGuaranteeService _guaranteeService;

        #endregion Private Fields

        #region Public Constructors

        public GuaranteeController(IGuaranteeService guaranteeService, IGuaranteeFactory guaranteeFactory)
        {
            _guaranteeService = guaranteeService;
            _guaranteeFactory = guaranteeFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanGuaranteeCreate)]
        public virtual async Task<HttpResponseMessage> Create(GuaranteeCreateModel viewModel)
        {
           await _guaranteeService.CreateByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanGuaranteeEdit)]
        public virtual async Task<HttpResponseMessage> Edit(RequestModel model)
        {
            var viewModel = await _guaranteeFactory.PrepareEditModelAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanGuaranteeEdit)]
        public virtual async Task<HttpResponseMessage> Edit(GuaranteeEditModel viewModel)
        {
            await _guaranteeService.EditByViewMoodelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanGuaranteeDeleteAjax)]
        public virtual async Task<HttpResponseMessage> DeleteAjax(RequestModel model)
        {
            await _guaranteeService.DeleteByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanGuaranteeList)]
        public virtual async Task<HttpResponseMessage> List(GuaranteeSearchModel request)
        {
            var viewModel = await _guaranteeFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        #endregion Public Methods
    }
}