using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Advertise.Core.Types;

namespace Advertise.Core.Managers.File
{
    public interface IFileManager
    {
        Task<FileModel> CreateAsync(string name, string path);
        Task<IList<FileModel>> DeleteAsync(string[] fileNames, string path = null);
        Task<ContentResult> GetAsContentResultAsync(IList<FileModel> files);
        Task<IList<FileModel>> GetListAsync(string path);
        Task<IList<FileModel>> SaveAsync(IEnumerable<HttpPostedFileBase> files, string path = null);
        Task<FilePathResult> GetFileFromThumbAsync(string path);
        Task<FileModel> SaveFromUploaderAsync(HttpPostedFileBase file, string path = null);
        Task<IList<FileModel>> SaveImageAsync(IEnumerable<HttpPostedFileBase> files, string path = null, ImageProcessType imageType = ImageProcessType.Nan);
        Task<IList<FileModel>> SaveVideoAsync(IEnumerable<HttpPostedFileBase> files, string path = null);
        Task<IList<FileModel>> SaveAttachmentAsync(IEnumerable<HttpPostedFileBase> files, string path = null);
    }
}