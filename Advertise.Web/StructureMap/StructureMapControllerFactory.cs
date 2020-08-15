using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Advertise.Core.Infrastructure.DependencyManagement;
using Advertise.Web.Utilities.HiddenField;

namespace Advertise.Web.StructureMap
{
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        #region Fields

        private readonly IEncryptSettingsProvider _settings;

        #endregion

        #region Ctor

        public StructureMapControllerFactory()
        {
            _settings = new EncryptSettingsProvider();
        }

        #endregion

        #region override GetControllerInstance

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                // todo: route pattern is correct, but controller not found
                return base.GetControllerInstance(requestContext, null);
            }
            var controller = ContainerManager.Container.GetInstance(controllerType) as System.Web.Mvc.Controller;
            if (controller != null)
            {
                // todo: disabled for using cache
                //controller.TempDataProvider = StructureMapObjectFactory.Container.GetInstance<ITempDataProvider>();
            }
            return controller;
        }

        #endregion

        #region override CreateController

        private IRijndaelStringEncrypter GetDecrypter(RequestContext requestContext)
        {
            var decrypter = new RijndaelStringEncrypter(_settings, requestContext.GetActionKey());
            return decrypter;
        }

        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            var routeData = requestContext.RouteData;
            if (routeData.Values.ContainsKey("MS_DirectRouteMatches"))
            {
                routeData = ((IEnumerable<RouteData>) routeData.Values["MS_DirectRouteMatches"]).First();
            }

            var parameters = requestContext.HttpContext.Request.Params;

            var encryptedParamKeys = new List<string>();
            if (parameters.AllKeys[0]!=null)
            {
                 encryptedParamKeys = parameters.AllKeys.Where(x => x.StartsWith(_settings.EncryptionPrefix)).ToList();
            }

            IRijndaelStringEncrypter decrypter = null;

            foreach (var key in encryptedParamKeys)
            {
                if (decrypter == null)
                {
                    decrypter = GetDecrypter(requestContext);
                }

                var oldKey = key.Replace(_settings.EncryptionPrefix, string.Empty);
                var oldValue = decrypter.Decrypt(parameters[key]);
                if (routeData.Values[oldKey] != null)
                {
                    if (routeData.Values[oldKey].ToString() != oldValue)
                        throw new ApplicationException("Form values is modified!");
                }

                routeData.Values[oldKey] = oldValue;
            }

            decrypter?.Dispose();

            return base.CreateController(requestContext, controllerName);
        }

        #endregion
    }
}