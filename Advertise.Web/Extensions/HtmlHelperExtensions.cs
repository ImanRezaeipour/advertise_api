using System.Text;
using System.Web.Mvc;
using Advertise.Core.Model.Common;

namespace Advertise.Web.Extensions
{
    public static class HtmlExtension
    {
        #region Public Methods

        public static MvcHtmlString Paging(this HtmlHelper html, BaseSearchModel pagedList, string url, string pagePlaceHolder)
        {
            StringBuilder sb = new StringBuilder();

            // only show paging if we have more items than the page size
            if (pagedList.TotalCount > pagedList.PageSize)
            {
                sb.Append("<ul class=\"paging\">");

                if (pagedList.HasPreviousPage)
                {
                    // previous link
                    sb.Append("<li class=\"prev\"><a href=\"");
                    sb.Append(url.Replace(pagePlaceHolder, pagedList.PageIndex.ToString()));
                    sb.Append("\" title=\"Go to Previous PageIndex\">prev</a></li>");
                }

                for (int i = 0; i < pagedList.PageSize; i++)
                {
                    sb.Append("<li>");
                    if (i == pagedList.PageIndex)
                    {
                        sb.Append("<span>").Append((i + 1).ToString()).Append("</span>");
                    }
                    else
                    {
                        sb.Append("<a href=\"");
                        sb.Append(url.Replace(pagePlaceHolder, (i + 1).ToString()));
                        sb.Append("\" title=\"Go to PageIndex ").Append((i + 1).ToString());
                        sb.Append("\">").Append((i + 1).ToString()).Append("</a>");
                    }
                    sb.Append("</li>");
                }

                if (pagedList.HasNextPage)
                {
                    // next link
                    sb.Append("<li class=\"next\"><a href=\"");
                    sb.Append(url.Replace(pagePlaceHolder, (pagedList.PageIndex + 2).ToString()));
                    sb.Append("\" title=\"Go to Next PageIndex\">next</a></li>");
                }

                sb.Append("</ul>");
            }

            return MvcHtmlString.Create(sb.ToString());
        }

        #endregion Public Methods
    }
}