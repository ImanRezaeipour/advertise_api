using Advertise.Core.Infrastructure.DependencyManagement;
using Advertise.Core.Model.Seos;
using Advertise.Core.Model.Settings;
using Advertise.Service.Localizations;
using Advertise.Service.Seos;
using Advertise.Service.Settings;

namespace Advertise.Web.ViewEngines.Razor
{
    public abstract class WebViewPage : WebViewPage<dynamic>
    {
    }

    public abstract class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        #region Public Methods

        public string L(string resource, string culture = null)
        {
            return LocalizationExtension.GetLocalize(resource, culture);
        }

        public string Lang(string resource, string culture = null)
        {
            return LocalizationExtension.GetLocalize(resource, culture);
        }

        public SeoSettingModel SeoSetting
        {
            get
            {
                var service = ContainerManager.Container.GetInstance<ISeoSettingService>();
                var viewModel = service.GetFirst();
                return viewModel;
            }
        }

        public SettingModel Setting
        {
            get
            {
                var service = ContainerManager.Container.GetInstance<ISettingService>();
                var viewModel = service.GetFirst();
                return viewModel;
            }
        }

        #endregion Public Methods
    }
}