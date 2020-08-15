using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Advertise.Core.Model.Users;
using Advertise.Service.Services.Permissions;
using Advertise.Service.Users;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class UserBudgetController : BaseController
    {
        #region Private Fields

        private readonly IUserBudgetService _userBudgetService;

        #endregion Private Fields

        #region Public Constructors

        public UserBudgetController(IUserBudgetService userBudgetService)
        {
            _userBudgetService = userBudgetService;
        }

        #endregion Public Constructors

        #region Public Methods

        [MvcAuthorize(PermissionConst.CanUserBudgetDetail)]
        public virtual async Task<HttpResponseMessage> Detail()
        {
            var viewModel = new UserBudgetDetailModel();
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        #endregion Public Methods
    }
}