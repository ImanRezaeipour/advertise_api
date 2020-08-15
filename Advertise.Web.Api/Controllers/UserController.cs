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
    public class UserController : BaseController
    {
        #region Private Fields

        private readonly IUserFactory _userFactory;
        private readonly IUserService _userService;

        #endregion Private Fields

        #region Public Constructors

        public UserController(IUserService userService,  IUserFactory userFactory)
        {
            _userService = userService;
            _userFactory = userFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        [MvcAuthorize(PermissionConst.CanUserDelete)]
        public virtual async Task<HttpResponseMessage> DeleteAjax(RequestModel model)
        {
            await _userService.DeleteByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public virtual async Task<HttpResponseMessage> Detail(RequestModel model)
        {
            var viewModel = await _userFactory.PrepareDetailModelAsync(model.UserName);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        public virtual async Task<HttpResponseMessage> DetailAjax(RequestModel model)
        {
            var userDetail = await _userFactory.PrepareDetailModelAsync(model.Id.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, userDetail);
        }

        [MvcAuthorize(PermissionConst.CanUserEdit)]
        public virtual async Task<HttpResponseMessage> Edit(RequestModel model)
        {
            var viewModel = await _userFactory.PrepareEditModelAsync(model.UserName);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanUserEdit)]
        public virtual async Task<HttpResponseMessage> Edit(UserEditModel viewModel)
        {
            await _userService.EditByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public virtual async Task<HttpResponseMessage> GetFileAjax(RequestModel model)
        {
            var file = await _userService.GetFileAsFineUploaderModelByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, file);
        }

        [MvcAuthorize(PermissionConst.CanUserList)]
        public virtual async Task<HttpResponseMessage> List(UserSearchModel request)
        {
            var viewModel = await _userFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanUserMyEdit)]
        public virtual async Task<HttpResponseMessage> MyEdit()
        {
            var viewModel = await _userFactory.PrepareEditModelAsync(isCurrentUser: true);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanUserMyEdit)]
        public virtual async Task<HttpResponseMessage> MyEdit(UserEditMeModel viewModel)
        {
            await _userService.EditMetaByViewModelAsync(viewModel, true);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public virtual async Task<HttpResponseMessage> Header()
        {
            var viewModel = await _userFactory.PrepareHeaderModelAsync();
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        #endregion Public Methods
    }
}