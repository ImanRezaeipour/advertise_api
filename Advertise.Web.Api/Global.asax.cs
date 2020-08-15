using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Advertise.Core.Infrastructure.DependencyManagement;
using Advertise.Core.Managers.Transaction;
using Advertise.Core.Model.Users;
using Advertise.Service.Users;
using Advertise.Web.Api.Configs;
using Advertise.Web.StructureMap;
using StructureMap.Web.Pipeline;

namespace Advertise.Web.Api
{
    public class MvcApplication : HttpApplication
    {
        #region Protected Methods

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            foreach (var task in ContainerManager.Container.GetAllInstances<IRunOnEachRequest>())
            {
                task.Execute();
            }
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            try
            {
                foreach (var task in ContainerManager.Container.GetAllInstances<IRunAfterEachRequest>())
                {
                    task.Execute();
                }
            }
            catch (Exception)
            {
                HttpContextLifecycle.DisposeAndClearAll();
            }
        }

        protected void Application_Error()
        {
            foreach (var task in ContainerManager.Container.GetAllInstances<IRunOnError>())
            {
                task.Execute();
            }
        }

        protected void Application_Init()
        {
            foreach (var task in ContainerManager.Container.GetAllInstances<IRunAtInit>())
            {
                task.Execute();
            }
        }

        protected void Application_Start()
        {
            try
            {
                StructureMapObjectFactory.DefaultContainer();
                
                RouteConfig.RegisterRoutes(RouteTable.Routes);
                
                FilterConfig.RegisterFilters(GlobalFilters.Filters);

                GlobalConfiguration.Configure(WebApiConfig.Register);
                
                //HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();
            }
            catch
            {
                // سبب ری استارت برنامه و آغاز مجدد آن با درخواست بعدی می‌شود
                HttpRuntime.UnloadAppDomain();
                throw;
            }
        }

        protected void Session_End()
        {
            ContainerManager.Container.GetInstance<IUserOnlineService>().DeleteBySessionId(Session.SessionID);
        }

        protected void Session_Start()
        {
            var viewModel = new UserOnlineModel
            {
                SessionId = Session.SessionID,
                IsActive = true
            };
            //  ContainerManager.Container.GetInstance<IUserOnlineService>().CreateByViewModel(viewModel);
        }
        
        private void SetPermissions(IEnumerable<string> permissions)
        {
            HttpContext.Current.User = new GenericPrincipal(HttpContext.Current.User.Identity, permissions.ToArray());
        }

        private bool ShouldIgnoreRequest()
        {
            string[] reservedPath =
            {
                "/__browserLink",
                "/Scripts",
                "/Content"
            };

            var rawUrl = HttpContext.Current.Request.RawUrl;
            if (reservedPath.Any(path => rawUrl.StartsWith(path, StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }

            return
                BundleTable.Bundles.Select(bundle => bundle.Path.TrimStart('~')).Any(bundlePath => rawUrl.StartsWith(bundlePath, StringComparison.OrdinalIgnoreCase));
        }

        #endregion Protected Methods
    }
}