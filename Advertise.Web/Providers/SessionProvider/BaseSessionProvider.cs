using System.Web;
using Advertise.Core.Managers.Cookie;

namespace Advertise.Web.Providers.SessionProvider
{

    public class BaseSessionProvider : ISessionProvider
    {
        private readonly HttpContextBase _httpContextBase;

        public BaseSessionProvider(HttpContextBase httpContextBase)
        {
            _httpContextBase = httpContextBase;
        }

        public object Get(string key)
        {
            return null;
        }

        public T Get<T>(string key) where T : class
        {
            return CookieExtention.DeserializeCookie<T>(_httpContextBase.GetCookieValue(key));
        }

        public void Remove(string key)
        {
            _httpContextBase.RemoveCookie(key);
        }

        public void RemoveAll()
        {
            _httpContextBase.RemoveAllCookies();
        }

        public void Store<T>(string key, T value) where T : class
        {
            _httpContextBase.AddCookie(key, CookieExtention.SerializeToBase64EncodedString(value));
        }
    }
}