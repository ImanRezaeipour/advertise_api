using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Advertise.Core.Model.Announces;
using Advertise.Core.Model.Common;
using Advertise.Service.Announces;
using Advertise.Service.Factory.Announces;

namespace Advertise.Web.Api.Controllers
{
    public class AnnounceController : BaseController
    {
        #region Private Fields

        private readonly IAnnounceFactory _announceFactory;
        private readonly IAnnounceService _announceService;

        #endregion Private Fields

        #region Public Constructors

        public AnnounceController(IAnnounceService announceService, IAnnounceFactory announceFactory)
        {
            _announceService = announceService;
            _announceFactory = announceFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual async Task<HttpResponseMessage> ApproveAjax(RequestModel model)
        {
            await _announceService.ApproveByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public virtual async Task<HttpResponseMessage> Create()
        {
            var viewModel = await _announceFactory.PrepareCreateModel();
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        public virtual async Task<HttpResponseMessage> Create(AnnounceCreateModel viewModel)
        {
            var result = await _announceService.CreateByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        public virtual async Task<HttpResponseMessage> CreateByAdmin(AnnounceCreateModel viewModel)
        {
            await _announceService.CreateByViewModelAsync(viewModel, true);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public virtual async Task<HttpResponseMessage> Edit(RequestModel model)
        {
            var viewModel = await _announceFactory.PrepareEditViewModelAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        public virtual async Task<HttpResponseMessage> EditApprove(AnnounceEditModel viewModel)
        {
            await _announceService.EditByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public virtual async Task<HttpResponseMessage> GetFileAjax(RequestModel model)
        {
            var files = await _announceService.GetFileAsFineUploaderModelByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, files);
        }

        public virtual async Task<HttpResponseMessage> RejectAjax(RequestModel model)
        {
            await _announceService.RejectByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        #endregion Public Methods
    }
}