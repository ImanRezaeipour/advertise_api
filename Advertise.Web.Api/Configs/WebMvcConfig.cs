using System;
using System.Linq;
using System.Web.Mvc;
using Advertise.Web.ModelBinders;
using Advertise.Web.StructureMap;

namespace Advertise.Web.Api.Configs
{
    public class WebMvcConfig
    {
        #region Public Methods

        public static void RegisterMvc()
        {
            MvcHandler.DisableMvcResponseHeader = true;

            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());

            var filterProider = FilterProviders.Providers.Single(p => p is FilterAttributeFilterProvider);
            FilterProviders.Providers.Remove(filterProider);
            FilterProviders.Providers.Add(new StructureMapFilterProvider());

            //var defaultJsonFactory = ValueProviderFactories.Factories.OfType<JsonValueProviderFactory>().FirstOrDefault();
            //var index = ValueProviderFactories.Factories.IndexOf(defaultJsonFactory);
            //ValueProviderFactories.Factories.Remove(defaultJsonFactory);
            //ValueProviderFactories.Factories.Insert(index, new JsonNetValueProviderFactory());

            System.Web.Mvc.ModelBinders.Binders.Add(typeof(DateTime), new PersianDateModelBinder());
            System.Web.Mvc.ModelBinders.Binders.Add(typeof(DateTime?), new PersianDateModelBinder());
            System.Web.Mvc.ModelBinders.Binders.Add(typeof(bool), new CheckBoxModelBinder());
            System.Web.Mvc.ModelBinders.Binders.Add(typeof(bool?), new CheckBoxModelBinder());

            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
        }

        #endregion Public Methods
    }
}