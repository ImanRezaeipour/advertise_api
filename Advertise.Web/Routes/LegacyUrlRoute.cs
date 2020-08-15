using System.Text;
using System.Web;
using System.Web.Routing;
using Advertise.Core.Infrastructure.DependencyManagement;
using Advertise.Service.Seos;

namespace Advertise.Web.Routes
{
    public class LegacyUrlRoute : RouteBase
    {
        #region Public Methods

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            if (httpContext.Request.Url == null)
                return null;

            var seoUrlService = ContainerManager.Container.GetInstance<ISeoUrlService>();
            var legacyUrls = seoUrlService.GetAll();
            var url = httpContext.Request.Url.PathAndQuery;
            var decodedUrl = HttpUtility.UrlDecode(url, Encoding.UTF8);
            if (!legacyUrls.ContainsKey(decodedUrl))
                return null;

            //httpContext.Response.Status = "301 Moved Permanently";
            //httpContext.Response.StatusCode = 301;
            //httpContext.Response.RedirectLocation = legacyUrls[httpContext.Request.Url.PathAndQuery];
            //httpContext.Response.End();
            httpContext.Response.RedirectPermanent(legacyUrls[HttpUtility.UrlDecode(httpContext.Request.Url.PathAndQuery, Encoding.UTF8)]);

            return null;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }

        #endregion Public Methods
    }
}