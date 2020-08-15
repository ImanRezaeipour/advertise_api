using System.Threading.Tasks;
using System.Web;

namespace Advertise.Core.Managers.Image
{
    public interface IImageValidator
    {
        Task<string> GetFormatImage(HttpPostedFileBase file);
        Task<string> GetFormatAttachment(HttpPostedFileBase file);
    }
}