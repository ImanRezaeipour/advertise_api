using System.Text;
using System.Web;

namespace Advertise.Service.Users
{
    public static class UserExtension
    {
        #region Public Methods

        public static string Base64ForUrlDecode(this string str)
        {
            var decbuff = HttpServerUtility.UrlTokenDecode(str);
            return decbuff != null ? Encoding.UTF8.GetString(decbuff) : null;
        }

        public static string Base64ForUrlEncode(this string str)
        {
            var encbuff = Encoding.UTF8.GetBytes(str);
            return HttpServerUtility.UrlTokenEncode(encbuff);
        }

        #endregion Public Methods
    }
}