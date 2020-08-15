using System.Threading.Tasks;
using System.Web;

namespace Advertise.Core.Managers.Attachment
{
    public class AttachmentValidator:IAttachmentValidator
    {
        public Task<string> GetFormatAsync(HttpPostedFileBase file)
        {
            return null;
        }

        public Task<string> GetSizeAsync(HttpPostedFileBase file)
        {
            return null;
        }
    }
}
