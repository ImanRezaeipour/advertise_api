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
    public class PlanDiscountController : BaseController
    {
        #region Private Fields

        private readonly IPlanDiscountFactory _planDiscountFactory;
        private readonly IPlanDiscountService _planDiscountService;

        #endregion Private Fields

        #region Public Constructors

        public PlanDiscountController(IPlanDiscountService planDiscountService, IPlanDiscountFactory planDiscountFactory)
        {
            _planDiscountService = planDiscountService;
            _planDiscountFactory = planDiscountFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanPlanDiscountCreate)]
        public virtual async Task<HttpResponseMessage> Create(PlanDiscountCreateModel viewModel)
        {
            await _planDiscountService.CreateByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public virtual async Task<HttpResponseMessage> GetPercentAjax(RequestModel model)
        {
            var planDiscountPercent = await _planDiscountService.GetPercentByCodeAsync(model.Code);
            return Request.CreateResponse(HttpStatusCode.OK, planDiscountPercent);
        }

        [MvcAuthorize(PermissionConst.CanPlanDiscountDeleteAjax)]
        public virtual async Task<HttpResponseMessage> DeleteAjax(RequestModel model)
        {
            await _planDiscountService.DeleteByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanPlanDiscountEdit)]
        public virtual async Task<HttpResponseMessage> Edit(PlanDiscountEditModel viewModel)
        {
            await _planDiscountService.EditByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanPlanDiscountEdit)]
        public virtual async Task<HttpResponseMessage> Edit(RequestModel model)
        {
            var viewModel = await _planDiscountFactory.PrepareEditModelAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanPlanDiscountList)]
        public virtual async Task<HttpResponseMessage> List(PlanDiscountSearchModel request)
        {
            var viewModel = await _planDiscountFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        #endregion Public Methods
    }
}