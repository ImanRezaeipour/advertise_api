using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Specifications;
using Advertise.Service.Factory.Specifications;
using Advertise.Service.Services.Permissions;
using Advertise.Service.Specifications;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class SpecificationController : BaseController
    {
        #region Private Fields

        private readonly ISpecificationFactory _specificationFactory;
        private readonly ISpecificationService _specificationService;

        #endregion Private Fields

        #region Public Constructors

        public SpecificationController(ISpecificationService specificationService, ISpecificationFactory specificationFactory)
        {
            _specificationService = specificationService;
            _specificationFactory = specificationFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanSpecificationCreate)]
        public virtual async Task<HttpResponseMessage> Create(SpecificationCreateModel viewModel)
        {
            await _specificationService.CreateByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanSpecificationCreate)]
        public virtual async Task<HttpResponseMessage> Create()
        {
            var viewModel = await _specificationFactory.PrepareCreateModelAsync();
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanSpecificationDeleteAjax)]
        public virtual async Task<HttpResponseMessage> DeleteAjax(RequestModel model)
        {
            await _specificationService.DeleteByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanSpecificationEdit)]
        public virtual async Task<HttpResponseMessage> Edit(RequestModel model)
        {
            var viewModel = await _specificationFactory.PrepareEditModelAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanSpecificationEdit)]
        public virtual async Task<HttpResponseMessage> Edit(SpecificationEditModel viewModel)
        {
            await _specificationService.EditByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanSpecificationGetListByCategoryAjax)]
        public virtual async Task<HttpResponseMessage> GetListByCategoryAjax(RequestModel model)
        {
            var specifications = await _specificationService.GetObjectByCategoryAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, specifications);
        }

        public virtual async Task<HttpResponseMessage> GetListByCategory(RequestModel model)
        {
            var viewModel= await _specificationService.GetObjectByCategoryAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        public virtual async Task<HttpResponseMessage> GetListSpecificationAjax(RequestModel model)
        {
            var specifications = await _specificationService.GetObjectByCategoryAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, specifications);
        }

        [MvcAuthorize(PermissionConst.CanSpecificationList)]
        public virtual async Task<HttpResponseMessage> List(SpecificationSearchModel request)
        {
            var viewModel = await _specificationFactory.PrepareListViewModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        #endregion Public Methods
    }
}