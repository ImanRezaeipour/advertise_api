using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Advertise.Web.Routes
{
    public class SubdomainRoute : RouteBase
    {
        #region Public Methods

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            if (httpContext.Request.Url == null)
                return null;

            if (httpContext.Request.Url.Host == "localhost")
                return null;

            var urlPathSeparators = httpContext.Request.Url.PathAndQuery.Split('/');
            if (urlPathSeparators.Length > 2)
                return null;

            var url = httpContext.Request.Headers["HOST"];
            var urlSeparates = url.Split('.');
            if (urlSeparates.First().Equals("mail") || urlSeparates.First().Equals("blog") || urlSeparates.First().Equals("www"))
                return null;

            if (urlSeparates.Length > 2)
            {
                var companyRouteData = new RouteData(this, new MvcRouteHandler());
                companyRouteData.Values.Add("controller", "Company");
                companyRouteData.Values.Add("action", "Detail");
                companyRouteData.Values.Add("alias", urlSeparates.First());
                companyRouteData.Values.Add("lang", "fa");

                return companyRouteData;
            }

            return null;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            //Implement your formating Url formating here
            return null;
        }

        #endregion Public Methods
    }
}