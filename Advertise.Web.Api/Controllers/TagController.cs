using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Tags;
using Advertise.Service.Factory.Tags;
using Advertise.Service.Services.Permissions;
using Advertise.Service.Tags;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class TagController : BaseController
    {
        #region Private Fields

        private readonly ITagFactory _tagFactory;
        private readonly ITagService _tagService;

        #endregion Private Fields

        #region Public Constructors

        public TagController(ITagService tag, ITagFactory tagFactory)
        {
            _tagService = tag;
            _tagFactory = tagFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        [MvcAuthorize(PermissionConst.CanTagCreate)]
        [HttpPost]
        public virtual async Task<HttpResponseMessage> Create(TagCreateModel viewModel)
        {
            await _tagService.CreateByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanTagCreate)]
        public virtual async Task<HttpResponseMessage> Create()
        {
            var viewModel = await _tagFactory.PrepareCreateModelAsync();
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanTagDeleteAjax)]
        public virtual async Task<HttpResponseMessage> DeleteAjax(RequestModel model)
        {
            await _tagService.DeleteByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanTagEdit)]
        public virtual async Task<HttpResponseMessage> Edit(RequestModel model)
        {
            var viewModel = await _tagFactory.PrepareEditModelAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanTagEdit)]
        public virtual async Task<HttpResponseMessage> Edit(TagEditModel viewModel)
        {
            await _tagService.EditByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        public virtual async Task<HttpResponseMessage> GetFileAjax(RequestModel model)
        {
            var file = await _tagService.GetFileAsFineUploaderModelByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, file);
        }

        [MvcAuthorize(PermissionConst.CanTagList)]
        public virtual async Task<HttpResponseMessage> List(TagSearchModel request)
        {
            var viewModel = await _tagFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        #endregion Public Methods
    }
}