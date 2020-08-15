using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Advertise.Core.Constants;
using Advertise.Core.Model.Categories;
using Advertise.Core.Model.Common;
using Advertise.Service.Categories;
using Advertise.Service.Factory.Categories;
using Advertise.Service.Services.Permissions;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class CategoryController : BaseController
    {
        #region Private Fields

        private readonly ICategoryFactory _categoryFactory;
        private readonly ICategoryService _categoryService;

        #endregion Private Fields

        #region Public Constructors

        public CategoryController(ICategoryService categoryService, ICategoryFactory categoryFactory)
        {
            _categoryService = categoryService;
            _categoryFactory = categoryFactory;
        }

        #endregion Public Constructors

        #region Public Methods
      
        public virtual async Task<HttpResponseMessage> BreadCrumb(RequestModel model)
        {
            var viewModel = await  _categoryFactory.PrepareBreadCrumbModelAsync(model.Id, model.Title, model.IsAll);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanCategoryCreate)]
        public virtual async Task<HttpResponseMessage> Create(CategoryCreateModel viewModel)
        {
            await _categoryService.CreateByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanCategoryCreate)]
        public virtual async Task<HttpResponseMessage> Create()
        {
            var viewModel = await _categoryFactory.PrepareCreateModelAsync();
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }
      
        [MvcAuthorize(PermissionConst.CanCategoryDeleteAjax)]
        public virtual async Task<HttpResponseMessage> DeleteAjax(RequestModel model)
        {
            await _categoryService.DeleteByAliasAsync(model.Code);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public virtual async Task<HttpResponseMessage> Detail(RequestModel model)
        {
            var viewModel = await _categoryFactory.PrepareDetailModelAsync(model.Code, model.Slug);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanCategoryEdit)]
        public virtual async Task<HttpResponseMessage> Edit(RequestModel model)
        {  
            var viewModel = await _categoryFactory.PrepareEditModelAsync(model.Code);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanCategoryEdit)]
        public virtual async Task<HttpResponseMessage> Edit(CategoryEditModel viewModel)
        {
            await _categoryService.EditByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanCategoryGetCategoriesAjax)]
        public virtual async Task<HttpResponseMessage> GetCategoriesAjax(RequestModel model)
        {
            var categories = await _categoryService.GetSubCategoriesByParentIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, categories);
        }

        public virtual async Task<HttpResponseMessage> GetFileAjax(RequestModel model)
        {
            var files = await _categoryService.GetFileAsFineUploaderModelByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, files);
        }

        public virtual async Task<HttpResponseMessage> GetMainCategoriesAjax()
        {
            var categories = await _categoryService.GetMainCategoriesAsync();
            return Request.CreateResponse(HttpStatusCode.OK, categories);
        }
     
        public virtual async Task<HttpResponseMessage> GetRelatedCategoriesAjax(RequestModel model)
        {
            var categories = await _categoryService.GetRaletedCategoriesByAliasAsync(model.Code);
            return Request.CreateResponse(HttpStatusCode.OK, categories);
        }

        [MvcAuthorize(PermissionConst.CanCategoryGetSubCatergoriesWithRootAjax)]
        public virtual async Task<HttpResponseMessage> GetSubCatergoriesWithRootAjax(RequestModel model)
        {
            var categories = await _categoryService.GetSubCatergoriesWithRootByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, categories);
        }

        public virtual async Task<HttpResponseMessage> GetTreeAjax()
        {
            var categories = await _categoryService.GetAllAsync();
            return Request.CreateResponse(HttpStatusCode.OK, categories);
        }

        [MvcAuthorize(PermissionConst.CanCategoryList)]
        public virtual async Task<HttpResponseMessage> List(CategorySearchModel request)
        {
            var viewModel = await _categoryFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }
        
        [SkipRemoveWhiteSpaces]
        public virtual async Task<HttpResponseMessage> CategoryExcel()
        {
            var categories = await _categoryService.GetCategoryAsExcelAsync();
            return Request.CreateResponse(HttpStatusCode.OK, categories, new MediaTypeHeaderValue(MimeTypes.TextXlsx));
        }
        
        public virtual async Task<HttpResponseMessage> MainMenu()
        {
            var viewModel = await _categoryFactory.PrepareMainMenuModelAsync();
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }
        
        [HttpPost]
        public virtual async Task<HttpResponseMessage> CategoryExcel(HttpPostedFileBase file)
        {
            await _categoryService.ImportCategoriesFromXlsxAsync(file.InputStream);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        #endregion Public Methods
    }
}