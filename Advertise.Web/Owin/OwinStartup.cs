using System;
using System.Web.Http;
using Advertise.Core.Configuration;
using Advertise.Core.Infrastructure.DependencyManagement;
using Advertise.Service.Users;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using StructureMap.Web;

namespace Advertise.Web.Owin
{
    public class OwinStartup
    {
        #region Public Methods

        public static void Configuration(IAppBuilder app)
        {
            const int twoWeeks = 14;

            ContainerManager.Container.Configure(config => config.For<IDataProtectionProvider>().HybridHttpOrThreadLocalScoped().Use(() => app.GetDataProtectionProvider()));

            //appBuilder.CreatePerOwinContext(() => ContainerManager.Container.GetInstance<IUserService>());
            
            // OWIN Section
            HttpConfiguration config2 = new HttpConfiguration();
            
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/GetToken"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new OwinAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            
            //WebApiConfig.Register(config2);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config2);
            
            
            app.SetDefaultSignInAsAuthenticationType(DefaultAuthenticationTypes.ExternalCookie);

           // appBuilder.UseCookieAuthentication(new CookieAuthenticationOptions
           // {
           //     AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
           //     LoginPath = new PathString("/fa/account/login"),
           //     ExpireTimeSpan = TimeSpan.FromDays(twoWeeks),
           //     SlidingExpiration = true,
           //     CookieName = "_novinak",
           //     CookieDomain = ContainerManager.Container.GetInstance<IConfigurationManager>().CookieIsLocalhost ? "localhost" : ".novinak.com",
           //     Provider = new CookieAuthenticationProvider
           //     {
           //         OnValidateIdentity = ContainerManager.Container.GetInstance<IUserService>().OnValidateIdentity()
           //     }
           // });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            var googleOAuth2AuthenticationOptions = new GoogleOAuth2AuthenticationOptions
            {
                ClientId = ContainerManager.Container.GetInstance<IConfigurationManager>().GoogleClientId,
                ClientSecret = ContainerManager.Container.GetInstance<IConfigurationManager>().GoogleCientSecret,
                Provider = new GoogleOAuth2AuthenticationProvider(),
                CallbackPath = new PathString("/signin-google")
            };

            app.UseGoogleAuthentication(googleOAuth2AuthenticationOptions);

            //StructureMapObjectFactory.Container.GetInstance<IRoleService>().SeedDatabase();

            //StructureMapObjectFactory.Container.GetInstance<IUserService>().SeedDatabase();
        }

        private static object GetUserServiceInstance()
        {
            return ContainerManager.Container.GetInstance<IUserService>();
        }

        #endregion Public Methods
    }
}