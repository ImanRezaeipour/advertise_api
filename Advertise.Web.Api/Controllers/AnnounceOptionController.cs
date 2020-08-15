using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Advertise.Core.Model.Announces;
using Advertise.Core.Model.Common;
using Advertise.Service.Announces;
using Advertise.Service.Factory.Announces;
using Advertise.Service.Services.Permissions;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class AdsOptionController : BaseController
    {
        #region Private Fields

        private readonly IAnnounceOptionFactory _announceOptionFactory;
        private readonly IAnnounceOptionService _announceOptionService;

        #endregion Private Fields

        #region Public Constructors

        public AdsOptionController(IAnnounceOptionService announceOptionService, IAnnounceOptionFactory announceOptionFactory)
        {
            _announceOptionService = announceOptionService;
            _announceOptionFactory = announceOptionFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual async Task<HttpResponseMessage> Create()
        {
            var viewModel = await _announceOptionFactory.PrepareCreateModelAsync();
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        public virtual async Task<HttpResponseMessage> Create(AnnounceOptionCreateModel viewModel)
        {
            await _announceOptionService.CreateByModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanAdsOptionEdit)]
        [HttpPost]
        public virtual async Task<HttpResponseMessage> Edit(AnnounceOptionEditModel viewModel)
        {
            await _announceOptionService.EditByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanAdsOptionEdit)]
        public virtual async Task<HttpResponseMessage> Edit(RequestModel model)
        {
            var viewModel = await _announceOptionFactory.PrepareEditModelAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }
      
        [MvcAuthorize(PermissionConst.CanAdsOptionGetOptionAjax)]
        public virtual async Task<HttpResponseMessage> GetOptionAjax(RequestModel model)
        {
            var result = await _announceOptionService.GetAsSelectListAsync(model.AnnounceType, model.AnnouncePositionType);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        
        [MvcAuthorize(PermissionConst.CanAdsOptionGetPriceAjax)]
        public virtual async Task<HttpResponseMessage> GetPriceAjax(RequestModel model)
        {
            var finalPrice = await _announceOptionService.GetPriceByIdAsync(model.Id, model.DurationType);
            return Request.CreateResponse(HttpStatusCode.OK, finalPrice);
        }

        [MvcAuthorize(PermissionConst.CanAdsOptionList)]
        public virtual async Task<HttpResponseMessage> List(AnnounceOptionSearchModel request)
        {
            var viewModel = await _announceOptionFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        #endregion Public Methods
    }
}