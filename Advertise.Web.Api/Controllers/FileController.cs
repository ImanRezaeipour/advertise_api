using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Advertise.Core.Managers.File;
using Advertise.Service.Services.Permissions;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class FileController : BaseController
    {
        #region Private Fields

        private readonly IFileManager _fileManager;

        #endregion Private Fields

        #region Public Constructors

        public FileController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPost]
        [MvcAuthorize(PermissionConst.CanFileCreateFromUploadAjax)]
        public virtual async Task<HttpResponseMessage> CreateFromUploadAjax(string name, string path)
        {
            var webPath = Path.Combine(HostingEnvironment.MapPath(FileConst.UploadPath), path);
            var folder = await _fileManager.CreateAsync(name, webPath);
            var thumbPath = Path.Combine(HostingEnvironment.MapPath(FileConst.ThumbPath), path);
            var thumbFolder = await _fileManager.CreateAsync(name, thumbPath);
            return Request.CreateResponse(HttpStatusCode.OK , folder);
        }
    
        [HttpPost]
        public virtual async Task<HttpResponseMessage> DeleteFromImageWebAjax(string[] fileNames)
        {
            var path = HostingEnvironment.MapPath(FileConst.ImagesWebPath);
            await _fileManager.DeleteAsync(fileNames, path);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public virtual async Task<HttpResponseMessage> DeleteFromUploadAjax(string[] fileNames)
        {
            var path = HostingEnvironment.MapPath(FileConst.UploadPath);
            await _fileManager.DeleteAsync(fileNames, path);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanFileGetFileFromThumb)]
        public virtual async Task<HttpResponseMessage> GetFileFromThumb(string path)
        {
            var file = await _fileManager.GetFileFromThumbAsync(path);
            return Request.CreateResponse(HttpStatusCode.OK, file, new MediaTypeHeaderValue("image/png"));
        }

        [MvcAuthorize(PermissionConst.CanFileGetFileFromUpload)]
        public virtual async Task<HttpResponseMessage> GetFileFromUpload(string path)
        {
            var name = Path.GetFileName(path);
            var directory = Path.GetDirectoryName(path);
            var uploadPath = HostingEnvironment.MapPath(FileConst.UploadPath);
            path = Path.Combine(uploadPath, directory, name);
            return Request.CreateResponse(HttpStatusCode.OK, path, new MediaTypeHeaderValue("image/png"));
        }

        [MvcAuthorize(PermissionConst.CanFileListFromUploadAjax)]
        public virtual async Task<HttpResponseMessage> ListFromUploadAjax(string path)
        {
            var uploadPath = HostingEnvironment.MapPath(FileConst.UploadPath);
            path = Path.Combine(uploadPath, path);
            var files = await _fileManager.GetListAsync(path);
            return Request.CreateResponse(HttpStatusCode.OK, files);
        }

        [HttpPost]
        public virtual async Task<HttpResponseMessage> SaveFromAttachmentAjax(IEnumerable<HttpPostedFileBase> files)
        {
            var path = HostingEnvironment.MapPath(FileConst.AttachmentPath);
            await _fileManager.SaveAsync(files, path);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public virtual async Task<HttpResponseMessage> SaveFromImageWebAjax(IEnumerable<HttpPostedFileBase> files)
        {
            var path = HostingEnvironment.MapPath(FileConst.ImagesWebPath);
            var result = await _fileManager.SaveAsync(files, path);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        public virtual async Task<HttpResponseMessage> SaveFromUploadAjax(IEnumerable<HttpPostedFileBase> files)
        {
            var path = HostingEnvironment.MapPath(FileConst.UploadPath);
            await _fileManager.SaveAsync(files, path);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
     
        [MvcAuthorize(PermissionConst.CanFileSaveFromUploaderAjax)]
        public virtual async Task<HttpResponseMessage> SaveFromUploaderAjax(HttpPostedFileBase file, string path)
        {
            var result = await _fileManager.SaveFromUploaderAsync(file, path);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        #endregion Public Methods
    }
}