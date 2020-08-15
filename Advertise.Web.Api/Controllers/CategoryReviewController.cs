using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Advertise.Core.Model.Categories;
using Advertise.Service.Categories;
using Advertise.Service.Factory.Categories;
using Advertise.Service.Services.Permissions;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class CategoryReviewController : BaseController
    {
        #region Private Fields

        private readonly ICategoryReviewFactory _categoryReviewFactory;
        private readonly ICategoryReviewService _categoryReviewService;

        #endregion Private Fields

        #region Public Constructors

      
        public CategoryReviewController(ICategoryReviewService categoryReviewService, ICategoryReviewFactory categoryReviewFactory)
        {
            _categoryReviewService = categoryReviewService;
            _categoryReviewFactory = categoryReviewFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanCategoryReviewCreate)]
        public virtual async Task<HttpResponseMessage> Create(CategoryReviewCreateModel viewModel)
        {
            await _categoryReviewService.CreateByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanCategoryReviewDeleteAjax)]
        public virtual async Task<HttpResponseMessage> DeleteAjax(Guid? id)
        {
            await _categoryReviewService.DeleteByIdAsync(id.Value);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanCategoryReviewEdit)]
        public virtual async Task<HttpResponseMessage> Edit(Guid? id)
        {
            var viewModel = await _categoryReviewFactory.PrepareEditModelAsync(id.Value);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanCategoryReviewEdit)]
        public virtual async Task<HttpResponseMessage> Edit(CategoryReviewEditModel viewModel)
        {
            await _categoryReviewService.EditByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanCategoryReviewList)]
        public virtual async Task<HttpResponseMessage> List(CategoryReviewSearchModel request)
        {
            var viewModel = await _categoryReviewFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        #endregion Public Methods
    }
}