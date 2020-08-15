using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Plans;
using Advertise.Service.Factory.Plans;
using Advertise.Service.Plans;
using Advertise.Service.Services.Permissions;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class PlanController : BaseController
    {
        #region Private Fields

        private readonly IPlanFactory _planFactory;
        private readonly IPlanService _planService;

        #endregion Private Fields

        #region Public Constructors

        public PlanController(IPlanService planService, IPlanFactory planFactory)
        {
            _planService = planService;
            _planFactory = planFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        [MvcAuthorize(PermissionConst.CanPlanCreate)]
        public virtual async Task<HttpResponseMessage> Create()
        {
            var viewModel = await _planFactory.PrepareCreateModelAsync();
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanPlanCreate)]
        public virtual async Task<HttpResponseMessage> Create(PlanCreateModel viewModel)
        {
            await _planService.CreateByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanPlanDeleteAjax)]
        public virtual async Task<HttpResponseMessage> DeleteAjax(RequestModel model)
        {
            await _planService.DeleteByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanPlanEdit)]
        public virtual async Task<HttpResponseMessage> Edit(RequestModel model)
        {
            var viewModel = await _planFactory.PrepareEditModelAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanPlanEdit)]
        public virtual async Task<HttpResponseMessage> Edit(PlanEditModel viewModel)
        {
            await _planService.EditByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanPlanList)]
        public virtual async Task<HttpResponseMessage> List(PlanSearchModel request)
        {
            var viewModel = await _planFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        #endregion Public Methods
    }
}