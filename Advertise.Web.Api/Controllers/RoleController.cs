using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Roles;
using Advertise.Service.Factory.Roles;
using Advertise.Service.Roles;
using Advertise.Service.Services.Permissions;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class RoleController : BaseController
    {
        #region Private Fields

        private readonly IRoleFactory _roleFactory;
        private readonly IRoleService _roleService;

        #endregion Private Fields

        #region Public Constructors

        public RoleController(IRoleService roleService, IRoleFactory roleFactory)
        {
            _roleService = roleService;
            _roleFactory = roleFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanRoleCreate)]
        public virtual async Task<HttpResponseMessage> Create(RoleCreateModel viewModel)
        {
            await _roleService.CreateByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanRoleDeleteAjax)]
        public virtual async Task<HttpResponseMessage> DeleteAjax(RequestModel model)
        {
            await _roleService.DeleteByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanRoleEdit)]
        public virtual async Task<HttpResponseMessage> Edit(RequestModel model)
        {
            var viewModel = await _roleFactory.PrepareEditModelAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanRoleEdit)]
        public virtual async Task<HttpResponseMessage> Edit(RoleEditModel viewModel)
        {
            await _roleService.EditByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanRoleList)]
        public virtual async Task<HttpResponseMessage> List(RoleSearchModel request)
        {
            var viewModel = await _roleFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        #endregion Public Methods
    }
}