using System.Threading.Tasks;
using System.Web;

namespace Advertise.Core.Managers.Attachment
{
    public interface IAttachmentValidator
    {
        Task<string> GetFormatAsync(HttpPostedFileBase file);
        Task<string> GetSizeAsync(HttpPostedFileBase file);
    }
}