using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Advertise.Core.Model.Common;
using Advertise.Service.Categories;
using Advertise.Service.Factory.Categories;
using Advertise.Service.Services.Permissions;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public partial class CategoryFollowController : BaseController
    {
        #region Private Fields

        private readonly ICategoryFollowFactory _categoryFollowFactory;
        private readonly ICategoryFollowService _categoryFollowService;

        #endregion Private Fields

        #region Public Constructors

        public CategoryFollowController(ICategoryFollowService categoryFollowService, ICategoryFollowFactory categoryFollowFactory)
        {
            _categoryFollowService = categoryFollowService;
            _categoryFollowFactory = categoryFollowFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual async Task<HttpResponseMessage> CountFollowAjax(RequestModel model)
        {
            var follows = await _categoryFollowService.CountAllFollowByCategoryIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, follows);
        }

        public virtual async Task<HttpResponseMessage> MyFollowList()
        {
            var viewModel = await _categoryFollowFactory.PrepareListModelAsync(true);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }
       
        [MvcAuthorize(PermissionConst.CanCategoryFollowMyIsFollowAjax)]
        public virtual async Task<HttpResponseMessage> MyIsFollowAjax(RequestModel model)
        {
            var isFollow = await _categoryFollowService.IsFollowCurrentUserByCategoryIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, isFollow);
        }
     
        [MvcAuthorize(PermissionConst.CanCategoryFollowMyToggleFollowAjax)]
        public virtual async Task<HttpResponseMessage> MyToggleAjax(RequestModel model)
        {
            await _categoryFollowService.ToggleFollowCurrentUserByCategoryIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        #endregion Public Methods
    }
}