using System.IO;
using System.Web;

namespace Advertise.Core.Helpers
{
    public static class FileHelper
    {
        #region Public Methods

        public static string FileSize(string filePath)
        {
            return filePath != null ? new FileInfo(HttpContext.Current.Server.MapPath(filePath)).Length.ToString() : "0";
        }

        #endregion Public Methods
    }
}