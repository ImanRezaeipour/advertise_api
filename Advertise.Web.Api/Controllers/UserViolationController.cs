using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Advertise.Core.Model.Users;
using Advertise.Service.Factory.Users;
using Advertise.Service.Services.Permissions;
using Advertise.Service.Users;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class UserViolationController : BaseController
    {
        #region Private Fields

        private readonly IUserViolationFactory _userViolationFactory;
        private readonly IUserViolationService _userViolationService;

        #endregion Private Fields

        #region Public Constructors

        public UserViolationController(IUserViolationService userViolationService, IUserViolationFactory userViolationFactory)
        {
            _userViolationService = userViolationService;
            _userViolationFactory = userViolationFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanUserViolationCreate)]
        public virtual async Task<HttpResponseMessage> Create(UserViolationCreateModel viewModel)
        {
            await _userViolationService.CreateByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanUserViolationDeleteAjax)]
        public virtual async Task<HttpResponseMessage> DeleteAjax(Guid? id)
        {
            await _userViolationService.DeleteByIdAsync(id.Value);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanUserViolationEdit)]
        public virtual async Task<HttpResponseMessage> Edit(UserViolationEditModel viewModel)
        {
            await _userViolationService.EditByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanUserViolationList)]
        public virtual async Task<HttpResponseMessage> List(UserViolationSearchModel request)
        {
            var viewModel = await _userViolationFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        #endregion Public Methods
    }
}