using System.Web;

namespace Advertise.Web.Utilities.MvcPaging
{
    public static class PagerOptionsBuilderExtensions
    {
        public static PagerOptionsBuilder AddFromQueryString(this PagerOptionsBuilder builder, HttpRequestBase request)
        {
            foreach (string item in request.QueryString)
            {
                if(item != "pageindex")
                    builder.AddRouteValue(item, request.QueryString[item]);
            }
            return builder;
        }
    }
}
