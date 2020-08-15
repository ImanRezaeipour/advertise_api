using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Advertise.Web.Routes;

namespace Advertise.Web.Api.Configs
{
    public class RouteConfig
    {
        #region Public Methods

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("{resource}.ico");
            routes.IgnoreRoute("{resource}.png");
            routes.IgnoreRoute("{resource}.jpg");
            routes.IgnoreRoute("{resource}.gif");
            routes.IgnoreRoute("{resource}.txt");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.LowercaseUrls = true;

            routes.Add(new LegacyUrlRoute());
            
            routes.MapMvcAttributeRoutes();

            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}",
                defaults: new {  id = RouteParameter.Optional }
            );

            routes.MapRoute(
                name: "NotFound",
                url: "notfound",
                defaults: new { lang = "fa", area = "", controller = "Error", action = "NotFound", id = UrlParameter.Optional },
                namespaces: new[] { "Advertise.Web.Controllers" }
            );

            routes.MapRoute(
                name: "CompanyDetail",
                url: "{lang}/company/nco-{alias}/{slug}",
                defaults: new { lang = "fa", area = "", controller = "Company", action = "Detail", slug = UrlParameter.Optional },
                namespaces: new[] { "Advertise.Web.Controllers" }
            ).RouteHandler = new DefaultCultureRouteHandler();

            routes.MapRoute(
                name: "CompanyEdit",
                url: "{lang}/company/edit/nco-{alias}",
                defaults: new { lang = "fa", area = "", controller = "Company", action = "Edit" },
                namespaces: new[] { "Advertise.Web.Controllers" }
            ).RouteHandler = new DefaultCultureRouteHandler();

            routes.MapRoute(
                name: "CompanyEditMe",
                url: "{lang}/mycompany",
                defaults: new { lang = "fa", area = "", controller = "Company", action = "MyEdit" },
                namespaces: new[] { "Advertise.Web.Controllers" }
            ).RouteHandler = new DefaultCultureRouteHandler();

            routes.MapRoute(
                name: "CategoryDetail",
                url: "{lang}/category/nca-{alias}/{slug}",
                defaults: new { lang = "fa", area = "", controller = "Category", action = "Detail", slug = UrlParameter.Optional },
                namespaces: new[] { "Advertise.Web.Controllers" }
            ).RouteHandler = new DefaultCultureRouteHandler();

            routes.MapRoute(
                name: "CategoryEdit",
                url: "{lang}/category/edit/nca-{alias}",
                defaults: new { lang = "fa", area = "", controller = "Category", action = "Edit" },
                namespaces: new[] { "Advertise.Web.Controllers" }
            ).RouteHandler = new DefaultCultureRouteHandler();

            routes.MapRoute(
                name: "ProductDetail",
                url: "{lang}/product/npr-{code}/{slug}",
                defaults: new { lang = "fa", area = "", controller = "Product", action = "Detail", slug = UrlParameter.Optional },
                namespaces: new[] { "Advertise.Web.Controllers" }
            ).RouteHandler = new DefaultCultureRouteHandler();

            routes.MapRoute(
                name: "ProductEdit",
                url: "{lang}/product/edit/npr-{code}",
                defaults: new { lang = "fa", area = "", controller = "Product", action = "Edit" },
                namespaces: new[] { "Advertise.Web.Controllers" }
            ).RouteHandler = new DefaultCultureRouteHandler();

            routes.MapRoute(
                name: "ProductEditMe",
                url: "{lang}/product/update/npr-{code}",
                defaults: new { lang = "fa", area = "", controller = "Product", action = "MyEdit" },
                namespaces: new[] { "Advertise.Web.Controllers" }
            ).RouteHandler = new DefaultCultureRouteHandler();


            routes.MapRoute(
                name: "UserDetail",
                url: "nu.{userName}/{lang}",
                defaults: new { lang = "fa", area = "", controller = "User", action = "Detail", slug = UrlParameter.Optional },
                namespaces: new[] { "Advertise.Web.Controllers" }
            ).RouteHandler = new DefaultCultureRouteHandler();

            routes.MapRoute(
                name: "UserEditMe",
                url: "{lang}/user/myaccount",
                defaults: new { lang = "fa", area = "", controller = "User", action = "MyEdit" },
                namespaces: new[] { "Advertise.Web.Controllers" }
            ).RouteHandler = new DefaultCultureRouteHandler();

            //routes.Add(new SubdomainRoute());

            routes.MapRoute(
                name: "Default",
                url: "{lang}/{controller}/{action}/{id}",
                defaults: new { lang = "fa", area = "", controller = "Home", action = "LandingPage", id = UrlParameter.Optional },
                namespaces: new[] { "Advertise.Web.Controllers" }
            ).RouteHandler = new DefaultCultureRouteHandler();


        }

        #endregion Public Methods
    }
}