using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Manufacturers;
using Advertise.Service.Factory.Manufacturers;
using Advertise.Service.Manufacturers;
using Advertise.Service.Services.Permissions;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class ManufacturerController : BaseController
    {
        #region Private Fields

        private readonly IManufacturerFactory _manufacturerFactory;
        private readonly IManufacturerService _manufacturerService;

        #endregion Private Fields

        #region Public Constructors

        public ManufacturerController(IManufacturerService manufacturerService, IManufacturerFactory manufacturerFactory)
        {
            _manufacturerService = manufacturerService;
            _manufacturerFactory = manufacturerFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanManufacturerCreate)]
        public virtual async Task<HttpResponseMessage> Create(ManufacturerCreateModel viewModel)
        {
            await _manufacturerService.CreateByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanManufacturerCreate)]
        public virtual async Task<HttpResponseMessage> Create()
        {
            var viewModel = await _manufacturerFactory.PrepareCreateModelAsync();
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanManufacturerEdit)]
        public virtual async Task<HttpResponseMessage> Edit(RequestModel model)
        {
            var viewModel = await _manufacturerFactory.PrepareEditModelAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanManufacturerEdit)]
        public virtual async Task<HttpResponseMessage> Edit(ManufacturerEditModel viewModel)
        {
            await _manufacturerService.EditByViewMoodelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanManufacturerDeleteAjax)]
        public virtual async Task<HttpResponseMessage> DeleteAjax(RequestModel model)
        {
            await _manufacturerService.DeleteByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanManufacturerList)]
        public virtual async Task<HttpResponseMessage> List(ManufacturerSearchModel request)
        {
            var viewModel = await _manufacturerFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        #endregion Public Methods
    }
}