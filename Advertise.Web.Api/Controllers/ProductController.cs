using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Products;
using Advertise.Core.Types;
using Advertise.Service.Factory.Products;
using Advertise.Service.Products;
using Advertise.Service.Services.Permissions;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class ProductController : BaseController
    {
        #region Private Fields

        private readonly IProductFactory _productFactory;
        private readonly IProductService _productService;

        #endregion Private Fields

        #region Public Constructors

        public ProductController(IProductService productService, IProductFactory productFactory)
        {
            _productService = productService;
            _productFactory = productFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanProductCreate)]
        public virtual async Task<HttpResponseMessage> Create(ProductCreateModel viewModel)
        {
            await _productService.CreateByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanProductCreate)]
        public virtual async Task<HttpResponseMessage> Create()
        {
            var viewModel = await _productFactory.PrepareCreateModelAsync();
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        public virtual async Task<HttpResponseMessage> BulkCreate()
        {
            var viewModel = await _productFactory.PrepareBulkCreateModelAsync();
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        public virtual async Task<HttpResponseMessage> BulkCreate(ProductBulkCreateModel viewModel)
        {
            await _productService.CreateBulkByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public virtual async Task<HttpResponseMessage> BulkEdit()
        {
            var viewModel = await _productFactory.PrepareBulkEditModelAsync();
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        public virtual async Task<HttpResponseMessage> BulkEdit(ProductBulkEditModel viewModel)
        {
            await _productService.EditBulkByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanProductDeleteAjax)]
        public virtual async Task<HttpResponseMessage> DeleteAjax(RequestModel model)
        {
            await _productService.DeleteByCodeAsync(model.Id.ToString());
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanProductEdit)]
        public virtual async Task<HttpResponseMessage> Edit(RequestModel model)
        {
            var viewModel = await _productFactory.PrepareEditModelAsync(model.Code);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        public virtual async Task<HttpResponseMessage> EditCatalog(RequestModel model)
        {
            var viewModel = await _productFactory.PrepareEditCatalogModelAsync(model.Code);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        public virtual async Task<HttpResponseMessage> EditCatalog(ProductEditCatalogModel viewModel)
        {
            await _productService.EditCatalogByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanProductEditApprove)]
        [HttpPost]
        public virtual async Task<HttpResponseMessage> EditApprove(ProductEditModel viewModel)
        {
            await _productService.EditApproveByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanProductEditReject)]
        [HttpPost]
        public virtual async Task<HttpResponseMessage> EditReject(ProductEditModel viewModel)
        {
           await _productService.EditRejectByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public virtual async Task<HttpResponseMessage> GetCountAjax(ProductSearchModel request)
        {
            request.State = StateType.Approved;
            var count = await _productService.CountByRequestAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, count);
        }

        public virtual async Task<HttpResponseMessage> GetCountByCategoryIdAjax(RequestModel model)
        {
            var result = await _productService.CountByCategoryIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        public virtual async Task<HttpResponseMessage> GetCountByCompanyIdAjax(RequestModel model)
        {
            var count = await _productService.CountByCompanyIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, count);
        }

        public virtual async Task<HttpResponseMessage> GetFileAjax(RequestModel model)
        {
            var file = await _productService.GetFileAsFineUploaderModelByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, file);
        }

        public virtual async Task<HttpResponseMessage> GetFilesAjax(RequestModel model)
        {
            var files = await _productService.GetFilesAsFineUploaderModelByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, files);
        }

        [MvcAuthorize(PermissionConst.CanProductList)]
        public virtual async Task<HttpResponseMessage> List(ProductSearchModel request)
        {
            var viewModel = await _productFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        public virtual async Task<HttpResponseMessage> ListMoreAjax(ProductSearchModel request)
        {
            request.State = StateType.Approved;
            var viewModel = await _productFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanProductMyDeleteAjax)]
        public virtual async Task<HttpResponseMessage> MyDeleteAjax(RequestModel model)
        {
            await _productService.DeleteByCodeAsync(model.Code, true);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanProductMyEdit)]
        public virtual async Task<HttpResponseMessage> MyEdit(RequestModel model)
        {
            var viewModel = await _productFactory.PrepareEditModelAsync(model.Code, true);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanProductMyEdit)]
        public virtual async Task<HttpResponseMessage> MyEdit(ProductEditModel viewModel)
        {
           await _productService.EditByViewModelAsync(viewModel, true);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanProductMyList)]
        public virtual async Task<HttpResponseMessage> MyList(ProductSearchModel request)
        {
            var viewModel = await _productFactory.PrepareListModelAsync(request, true);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        public virtual async Task<HttpResponseMessage> ProductReviewListAjax(RequestModel model)
        {
            var viewModel = await _productFactory.PrepareReviewListModelAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        public virtual async Task<HttpResponseMessage> Detail(RequestModel model)
        {
            var viewModel = await _productFactory.PrepareDetailModelAsync(model.Code);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        public virtual async Task<HttpResponseMessage> Search(ProductSearchModel request = null)
        {
            var requestSafe = request ?? new ProductSearchModel();
           // requestSafe.QueryString = Request.Url?.Query.UrlDecode();
            requestSafe.CategoryAlias = request.CategoryAlias ?? "Category-All";
           // requestSafe.GroupBy = product => product.Code;
            requestSafe.PageSize = PageSize.Count60;
            var viewModel = await _productFactory.PrepareSearchModelAsync(requestSafe);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        public virtual async Task<HttpResponseMessage> ApproveAjax(RequestModel model)
        {
            await _productService.SetStateByIdAsync(model.Id, StateType.Approved);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public virtual async Task<HttpResponseMessage> RejectAjax(RequestModel model)
        {
            await _productService.SetStateByIdAsync(model.Id, StateType.Rejected);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        #endregion Public Methods
    }
}