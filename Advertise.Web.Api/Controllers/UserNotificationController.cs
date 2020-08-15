using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Users;
using Advertise.Service.Factory.Users;
using Advertise.Service.Services.Permissions;
using Advertise.Service.Users;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class UserNotificationController : BaseController
    {
        #region Private Fields

        private readonly IUserNotificationFactory _notificationFactory;
        private readonly IUserNotificationService _notificationService;

        #endregion Private Fields

        #region Public Constructors

        public UserNotificationController(IUserNotificationService notificationService, IUserNotificationFactory notificationFactory)
        {
            _notificationService = notificationService;
            _notificationFactory = notificationFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        [MvcAuthorize(PermissionConst.CanNotificationDeleteAjax)]
        public virtual async Task<HttpResponseMessage> DeleteAjax(RequestModel model)
        {
            await _notificationService.DeleteByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
  
        [MvcAuthorize(PermissionConst.CanNotificationMyList)]
        public virtual async Task<HttpResponseMessage> MyList(UserNotificationSearchModel request)
        {
            var viewModel = await _notificationFactory.PrepareListModelAsync(request, true);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        #endregion Public Methods
    }
}