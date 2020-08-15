using System;
using System.Web.Mvc;

namespace Advertise.Web.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class RedirectToCanonicalUrlAttribute : FilterAttribute, IAuthorizationFilter
    {
        private const char QueryCharacter = '?';
        private const char SlashCharacter = '/';

        private readonly bool appendTrailingSlash;
        private readonly bool lowercaseUrls;

        public RedirectToCanonicalUrlAttribute(
            bool appendTrailingSlash,
            bool lowercaseUrls)
        {
            this.appendTrailingSlash = appendTrailingSlash;
            this.lowercaseUrls = lowercaseUrls;
        }

        public bool AppendTrailingSlash
        {
            get { return this.appendTrailingSlash; }
        }

        public bool LowercaseUrls
        {
            get { return this.lowercaseUrls; }
        }

        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (string.Equals(filterContext.HttpContext.Request.HttpMethod, "GET", StringComparison.Ordinal))
            {
                string canonicalUrl;
                if (!this.TryGetCanonicalUrl(filterContext, out canonicalUrl))
                {
                    this.HandleNonCanonicalRequest(filterContext, canonicalUrl);
                }
            }
        }

        protected virtual bool TryGetCanonicalUrl(AuthorizationContext filterContext, out string canonicalUrl)
        {
            bool isCanonical = true;

            Uri url = filterContext.HttpContext.Request.Url;
            canonicalUrl = url.ToString();
            int queryIndex = canonicalUrl.IndexOf(QueryCharacter);

            // If we are not dealing with the home page. Note, the home page is a special case and it doesn't matter
            // if there is a trailing slash or not. Both will be treated as the same by search engines.
            if (url.AbsolutePath.Length > 1)
            {
                if (queryIndex == -1)
                {
                    bool hasTrailingSlash = canonicalUrl[canonicalUrl.Length - 1] == SlashCharacter;

                    if (this.appendTrailingSlash)
                    {
                        // Append a trailing slash to the end of the URL.
                        if (!hasTrailingSlash && !this.HasNoTrailingSlashAttribute(filterContext))
                        {
                            canonicalUrl += SlashCharacter;
                            isCanonical = false;
                        }
                    }
                    else
                    {
                        // Trim a trailing slash from the end of the URL.
                        if (hasTrailingSlash)
                        {
                            canonicalUrl = canonicalUrl.TrimEnd(SlashCharacter);
                            isCanonical = false;
                        }
                    }
                }
                else
                {
                    bool hasTrailingSlash = canonicalUrl[queryIndex - 1] == SlashCharacter;

                    if (this.appendTrailingSlash)
                    {
                        // Append a trailing slash to the end of the URL but before the query string.
                        if (!hasTrailingSlash && !this.HasNoTrailingSlashAttribute(filterContext))
                        {
                            canonicalUrl = canonicalUrl.Insert(queryIndex, SlashCharacter.ToString());
                            isCanonical = false;
                        }
                    }
                    else
                    {
                        // Trim a trailing slash to the end of the URL but before the query string.
                        if (hasTrailingSlash)
                        {
                            canonicalUrl = canonicalUrl.Remove(queryIndex - 1, 1);
                            isCanonical = false;
                        }
                    }
                }
            }

            if (this.lowercaseUrls)
            {
                foreach (char character in canonicalUrl)
                {
                    if (this.HasNoLowercaseQueryStringAttribute(filterContext) && queryIndex != -1)
                    {
                        if (character == QueryCharacter)
                        {
                            break;
                        }

                        if (char.IsUpper(character) && !this.HasNoTrailingSlashAttribute(filterContext))
                        {
                            canonicalUrl = canonicalUrl.Substring(0, queryIndex).ToLower() +
                                canonicalUrl.Substring(queryIndex, canonicalUrl.Length - queryIndex);
                            isCanonical = false;
                            break;
                        }
                    }
                    else
                    {
                        if (char.IsUpper(character) && !this.HasNoTrailingSlashAttribute(filterContext))
                        {
                            canonicalUrl = canonicalUrl.ToLower();
                            isCanonical = false;
                            break;
                        }
                    }
                }
            }

            return isCanonical;
        }

        protected virtual void HandleNonCanonicalRequest(AuthorizationContext filterContext, string canonicalUrl)
        {
            filterContext.Result = new RedirectResult(canonicalUrl, true);
        }

        protected virtual bool HasNoTrailingSlashAttribute(AuthorizationContext filterContext)
        {
            return filterContext.ActionDescriptor.IsDefined(typeof(NoTrailingSlashAttribute), false) ||
                filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(NoTrailingSlashAttribute), false);
        }

        protected virtual bool HasNoLowercaseQueryStringAttribute(AuthorizationContext filterContext)
        {
            return filterContext.ActionDescriptor.IsDefined(typeof(NoLowercaseQueryStringAttribute), false) ||
                filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(NoLowercaseQueryStringAttribute), false);
        }
    }
}