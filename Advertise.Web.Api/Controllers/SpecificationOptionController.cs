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
    public class SpecificationOptionController : BaseController
    {
        #region Private Fields

        private readonly ISpecificationOptionFactory _specificationOptionFactory;
        private readonly ISpecificationOptionService _specificationOptionService;

        #endregion Private Fields

        #region Public Constructors

        public SpecificationOptionController(ISpecificationOptionService specificationOptionService, ISpecificationOptionFactory specificationOptionFactory)
        {
            _specificationOptionService = specificationOptionService;
            _specificationOptionFactory = specificationOptionFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanSpecificationOptionCreate)]
        public virtual async Task<HttpResponseMessage> Create(SpecificationOptionCreateModel viewModel)
        {
            await _specificationOptionService.CreateByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanSpecificationOptionCreate)]
        public virtual async Task<HttpResponseMessage> Create()
        {
            var viewModel = await _specificationOptionFactory.PrepareCreateModelAsync();
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanSpecificationOptionDeleteAjax)]
        public virtual async Task<HttpResponseMessage> DeleteAjax(RequestModel model)
        {
            await _specificationOptionService.DeleteByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanSpecificationOptionEdit)]
        public virtual async Task<HttpResponseMessage> Edit(RequestModel model)
        {
            var viewModel = await _specificationOptionFactory.PrepareEditModelAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanSpecificationOptionEdit)]
        public virtual async Task<HttpResponseMessage> Edit(SpecificationOptionEditModel viewModel)
        {
            await _specificationOptionService.EditByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanSpecificationOptionList)]
        public virtual async Task<HttpResponseMessage> List(SpecificationOptionSearchModel request)
        {
            var viewModel = await _specificationOptionFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        #endregion Public Methods
    }
}