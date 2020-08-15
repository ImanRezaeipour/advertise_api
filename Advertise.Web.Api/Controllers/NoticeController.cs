using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Notices;
using Advertise.Service.Factory.Notices;
using Advertise.Service.Notices;
using Advertise.Service.Services.Permissions;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class NoticeController : BaseController
    {
        #region Private Fields

        private readonly INoticeFactory _noticeFactory;
        private readonly INoticeService _noticeService;

        #endregion Private Fields

        #region Public Constructors

        public NoticeController(INoticeService noticeService, INoticeFactory noticeFactory)
        {
            _noticeService = noticeService;
            _noticeFactory = noticeFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        [MvcAuthorize(PermissionConst.CanNewsCreate)]
        [HttpPost]
        public virtual async Task<HttpResponseMessage> Create(NoticeCreateModel viewModel)
        {
            await _noticeService.CreateByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanNewsDeleteAjax)]
        public virtual async Task<HttpResponseMessage> DeleteAjax(RequestModel model)
        {
            await _noticeService.DeleteByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanNewsEdit)]
        public virtual async Task<HttpResponseMessage> Edit(RequestModel model)
        {
            var viewModel = await _noticeFactory.PrepareEditModelAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanNewsEdit)]
        public virtual async Task<HttpResponseMessage> Edit(NoticeEditModel viewModel)
        {
            await _noticeService.EditByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanNewsList)]
        public virtual async Task<HttpResponseMessage> List(NoticeSearchModel request)
        {
            var viewModel = await _noticeFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        #endregion Public Methods
    }
}