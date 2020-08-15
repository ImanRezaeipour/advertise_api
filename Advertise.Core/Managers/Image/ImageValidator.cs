using System.IO;
using System.Threading.Tasks;
using System.Web;

namespace Advertise.Core.Managers.Image
{
    public class ImageValidator : IImageValidator
    {
        public async Task<string> GetFormatImage(HttpPostedFileBase file)
        {
            string[] format = { ".jpg", ".png",".jpeg",".zip",".rar",".pdf" };
            var pathExtention = Path.GetExtension(file.FileName);
            foreach (string fileEx in format)
            {
                if (fileEx == pathExtention)
                    return null;
            }
            return "فرمت اشتباه است";
        }

        public async Task<string> GetFormatAttachment(HttpPostedFileBase file)
        {
            string[] format = {".zip", ".rar", ".pdf" };
            var pathExtention = Path.GetExtension(file.FileName);
            foreach (string fileEx in format)
            {
                if (fileEx == pathExtention)
                    return null;
            }
            return "فرمت اشتباه است";
        }
    }
}