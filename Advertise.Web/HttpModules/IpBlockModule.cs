using System;
using System.Collections.Specialized;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.Caching;
using Advertise.Core.Extensions;

namespace Advertise.Web.HttpModules
{
    public class IpBlockModule : IHttpModule
    {
        #region Private Fields

        private const string Blockedipsfile = "SiteConfig/blockedips.config";
        private const string Blockedipskey = "blockedips";
        private static Lazy<string> _blockedIpFileName;
        private readonly EventHandler _onBeginRequest;

        #endregion Private Fields

        #region Public Constructors

        public IpBlockModule()
        {
            _onBeginRequest = HandleBeginRequest;
        }

        #endregion Public Constructors

        #region Public Methods

        public static StringDictionary GetBlockedIPs(HttpContext context)
        {
            var ips = (StringDictionary)context.Cache[Blockedipskey];
            if (ips != null) return ips;
            ips = GetBlockedIPs(GetBlockedIPsFilePathFromCurrentContext(context));
            context.Cache.Insert(Blockedipskey, ips, new CacheDependency(GetBlockedIPsFilePathFromCurrentContext(context)));
            return ips;
        }

        public static StringDictionary GetBlockedIPs(string configPath)
        {
            var retval = new StringDictionary();
            using (var sr = new StreamReader(configPath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (line.Length != 0)
                    {
                        retval.Add(line, null);
                    }
                }
            }
            return retval;
        }

        public static string GetBlockedIPsFilePathFromCurrentContext(HttpContext context)
        {
            if (_blockedIpFileName.IsValueCreated)
                return _blockedIpFileName.Value;
            _blockedIpFileName = new Lazy<string>(() => context.Server.MapPath(Blockedipsfile),
                LazyThreadSafetyMode.ExecutionAndPublication);
            return _blockedIpFileName.Value;
        }

        void IHttpModule.Dispose()
        {
        }

        void IHttpModule.Init(HttpApplication context)
        {
            context.BeginRequest += _onBeginRequest;
        }

        #endregion Public Methods

        #region Private Methods

        private static void HandleBeginRequest(object sender, EventArgs evargs)
        {
            var app = sender as HttpApplication;

            var ipAddr = app?.Context.Request.GetIp();
            if (string.IsNullOrEmpty(ipAddr))
            {
                return;
            }

            var badIPs = GetBlockedIPs(app.Context);
            if (badIPs == null || !badIPs.ContainsKey(ipAddr)) return;
            app.Context.Response.StatusCode = 404;
            app.Context.Response.SuppressContent = true;
            app.Context.Response.End();
        }

        #endregion Private Methods
    }
}