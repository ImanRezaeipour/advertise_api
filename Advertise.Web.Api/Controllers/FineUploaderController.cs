using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Advertise.Core.Managers.File;
using Advertise.Core.Types;

namespace Advertise.Web.Api.Controllers
{
    public class FineUploaderController : BaseController
    {
        #region Private Fields

        private readonly IFileManager _fileManager;

        #endregion Private Fields

        #region Public Constructors

        public FineUploaderController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpDelete]
        public virtual async Task<HttpResponseMessage> Remove(Guid uuid)
        {
            var result = new
            {
                success = true
            };
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpDelete]
        public virtual async Task<HttpResponseMessage> RemoveAttachment(Guid uuid)
        {
            var result = new
            {
                success = true
            };
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpDelete]
        public virtual async Task<HttpResponseMessage> RemoveVideo(Guid uuid)
        {
            var result = new
            {
                success = true
            };
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        public virtual async Task<HttpResponseMessage> SaveToAttachmentWeb(IEnumerable<HttpPostedFileBase> qqfile)
        {
            if (qqfile == null || !qqfile.Any())
            {
                var badRequest = new
                {
                    success = false
                };
                return Request.CreateResponse(HttpStatusCode.OK, badRequest);
            }

            var path = HostingEnvironment.MapPath(FileConst.AttachmentPath);
            var file = await _fileManager.SaveAttachmentAsync(qqfile, path);
            var result = new
            {
                success = true,
                result = file
            };
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

    
        [HttpPost]
        public virtual async Task<HttpResponseMessage> SaveToImageWeb(IEnumerable<HttpPostedFileBase> qqfile, ImageProcessType imageType)
        {
            if (qqfile == null || !qqfile.Any())
            {
                var badRequest = new
                {
                    success = false
                };
                return Request.CreateResponse(HttpStatusCode.OK, badRequest);
            }

            var path = HostingEnvironment.MapPath(FileConst.ImagesWebPath);
            var file = await _fileManager.SaveImageAsync(qqfile, path, imageType);
            var result = new
            {
                success = true,
                result = file
            };
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        public virtual async Task<HttpResponseMessage> SaveToImageCatalogWeb(IEnumerable<HttpPostedFileBase> qqfile, ImageProcessType imageType)
        {
            if (qqfile == null || !qqfile.Any())
            {
                var badRequest = new
                {
                    success = false
                };
                return Request.CreateResponse(HttpStatusCode.OK, badRequest);
            }

            var path = HostingEnvironment.MapPath(FileConst.ImagesCatalogWebPath);
            var file = await _fileManager.SaveImageAsync(qqfile, path, imageType);
            var result = new
            {
                success = true,
                result = file
            };
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        public virtual async Task<HttpResponseMessage> SaveToVideoWeb(IEnumerable<HttpPostedFileBase> qqfile)
        {
            if (qqfile == null || !qqfile.Any())
            {
                var badRequest = new
                {
                    success = false
                };
                return Request.CreateResponse(HttpStatusCode.OK, badRequest);
            }

            var path = HostingEnvironment.MapPath(FileConst.VideosWebPath);
            var file = await _fileManager.SaveVideoAsync(qqfile, path);
            var result = new
            {
                success = true,
                result = file
            };
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        #endregion Public Methods
    }
}