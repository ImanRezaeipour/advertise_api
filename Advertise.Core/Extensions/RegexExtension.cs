using System.Text.RegularExpressions;

namespace Advertise.Core.Extensions
{
    public static class RegexExtension
    {
        #region Private Fields

        private static readonly Regex ContentRegex = new Regex(@"<\/?script[^>]*?>",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static readonly Regex HtmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        private static readonly Regex SafeStrRegex = new Regex(@"<script[^>]*?>[\s\S]*?<\/script>",
           RegexOptions.Compiled | RegexOptions.IgnoreCase);

        #endregion Private Fields

        #region Public Methods

        public static string CleanScriptsTagsAndContents(this string html)
        {
            return SafeStrRegex.Replace(html, "");
        }

        public static string CleanScriptTags(this string html)
        {
            return ContentRegex.Replace(html, string.Empty);
        }

        public static string CleanTags(this string html)
        {
            HtmlRegex.Replace(html, string.Empty);

            html = html.Replace("&nbsp;", " ");
            html = html.Replace("&zwnj;", " ");
            html = html.Replace("&quot;", " ");
            html = html.Replace("amp;", "");
            html = html.Replace("&laquo;", "«");
            html = html.Replace("&raquo;", "»");
            return html;
        }

        #endregion Public Methods
    }
}