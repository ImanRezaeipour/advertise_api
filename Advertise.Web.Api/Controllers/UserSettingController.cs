using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Users;
using Advertise.Service.Factory.Users;
using Advertise.Service.Services.Permissions;
using Advertise.Service.Users;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class UserSettingController : BaseController
    {
        #region Private Fields

        private readonly IUserSettingFactory _userSettingFactory;
        private readonly IUserSettingService _userSettingService;

        #endregion Private Fields

        #region Public Constructors

        public UserSettingController(IUserSettingService userSettingService, IUserSettingFactory userSettingFactory)
        {
            _userSettingService = userSettingService;
            _userSettingFactory = userSettingFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        [MvcAuthorize(PermissionConst.CanUserSettingEdit)]
        public virtual async Task<HttpResponseMessage> Edit(RequestModel model)
        {
            var viewModel = await _userSettingFactory.PrepareEditModelAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<HttpResponseMessage> Edit(UserSettingEditModel viewModel)
        {
            await _userSettingService.EditByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }


        [MvcAuthorize(PermissionConst.CanUserSettingList)]
        public virtual async Task<HttpResponseMessage> List(UserSettingSearchModel request)
        {
            var viewModel = await _userSettingFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanUserSettingMyEdit)]
        public virtual async Task<HttpResponseMessage> MyEdit()
        {
            var viewModel = await _userSettingFactory.PrepareEditModelAsync(isCurrentUser: true);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanUserSettingMyEdit)]
        public virtual async Task<HttpResponseMessage> MyEdit(UserSettingEditModel viewModel)
        {
            await _userSettingService.EditByViewModelAsync(viewModel, true);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        #endregion Public Methods
    }
}