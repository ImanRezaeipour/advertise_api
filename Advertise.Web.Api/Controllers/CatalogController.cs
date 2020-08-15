using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Advertise.Core.Constants;
using Advertise.Core.Model.Catalogs;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.ExportImport;
using Advertise.Service.Catalogs;
using Advertise.Service.Factory.Catalogs;
using Advertise.Service.Services.Permissions;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class CatalogController : BaseController
    {
        #region Private Fields

        private readonly ICatalogFactory _catalogFactory;
        private readonly ICatalogService _catalogService;

        #endregion Private Fields

        #region Public Constructors

        public CatalogController(ICatalogService catalogService, ICatalogFactory catalogFactory)
        {
            _catalogService = catalogService;
            _catalogFactory = catalogFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanCatalogCreate)]
        public virtual async Task<HttpResponseMessage> Create(CatalogCreateModel viewModel)
        {
            await _catalogService.CreateByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanCatalogCreate)]
        public virtual async Task<HttpResponseMessage> Create()
        {
            var viewModel = await _catalogFactory.PrepareCreateModelAsync();
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanCatalogDeleteAjax)]
        public virtual async Task<HttpResponseMessage> DeleteAjax(RequestModel model)
        {
            await _catalogService.DeleteByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanCatalogEdit)]
        public virtual async Task<HttpResponseMessage> Edit(RequestModel model)
        {
            var viewModel = await _catalogFactory.PrepareEditModelAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanCatalogEdit)]
        public virtual async Task<HttpResponseMessage> Edit(CatalogEditModel viewModel)
        {
            await _catalogService.EditByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanCatalogList)]
        public virtual async Task<HttpResponseMessage> List(CatalogSearchModel request)
        {
            var viewModel = await _catalogFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        public virtual async Task<HttpResponseMessage> GetFileAjax(RequestModel model)
        {
            var file = await _catalogService.GetFileAsFineUploaderModelByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, file);
        }

        public virtual async Task<HttpResponseMessage> GetFilesAjax(RequestModel model)
        {
            var files = await _catalogService.GetFilesAsFineUploaderModelByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, files);
        }
        
        [SkipRemoveWhiteSpaces]
        public virtual async Task<HttpResponseMessage> CatalogTemplateExcel(ExportIndexModel viewModel)
        {
            var catalogTemplate = await _catalogService.GetCatalogTemplateAsExcelAsync(viewModel.CategoryId);
            return Request.CreateResponse(HttpStatusCode.OK, catalogTemplate, new MediaTypeHeaderValue(MimeTypes.TextXlsx));
        }
        
        
        [HttpPost]
        public virtual async Task<HttpResponseMessage> CatalogExcel(ImportCatalogModel viewModel)
        {
            await _catalogService.ImportCatalogsFromXlsxAsync(viewModel.CatalogFile.InputStream, viewModel.CategoryId);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        #endregion Public Methods
    }
}