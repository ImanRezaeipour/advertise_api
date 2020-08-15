using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Permissions;
using Advertise.Service.Factory.Permissions;
using Advertise.Service.Permissions;
using Advertise.Service.Services.Permissions;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class PermissionController : BaseController
    {
        #region Private Fields

        private readonly IPermissionFactory _permissionFactory;
        private readonly IPermissionService _permissionService;

        #endregion Private Fields

        #region Public Constructors
     
        public PermissionController(IPermissionService permissionService, IPermissionFactory permissionFactory)
        {
            _permissionService = permissionService;
            _permissionFactory = permissionFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPost]
        public virtual async Task<HttpResponseMessage> Create(PermissionCreateModel viewModel)
        {
            await _permissionService.CreateByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public virtual async Task<HttpResponseMessage> Edit(RequestModel model)
        {
            var viewModel = await _permissionFactory.PrepareEditModelAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        public virtual async Task<HttpResponseMessage> Edit(PermissionEditModel viewModel)
        {
            await _permissionService.EditByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
      
        [MvcAuthorize(PermissionConst.CanPermissionGetListAjax)]
        public virtual async Task<HttpResponseMessage> GetListAjax()
        {
            var permissions = await _permissionService.GetAllPermissionsAsync();
            return Request.CreateResponse(HttpStatusCode.OK, permissions);
        }

        public virtual async Task<HttpResponseMessage> GetListByRoleIdAjax(RequestModel model)
        {
            var permissions = await _permissionService.GetAllTreeByRoleIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, permissions);
        }

        public virtual async Task<HttpResponseMessage> GetPermissionsAjax()
        {
            var permissions = await _permissionService.GetAllTreeAsync();
            return Request.CreateResponse(HttpStatusCode.OK, permissions);
        }

        public virtual async Task<HttpResponseMessage> List(PermissionSearchModel request)
        {
            var viewModel = await _permissionFactory.PrepareListModel(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        #endregion Public Methods
    }
}