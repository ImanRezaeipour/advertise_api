using System.Globalization;

namespace Advertise.Service.Localizations
{
    public static class LocalizationExtension
    {
        #region Public Methods

        public static string GetLocalize(string resource, string culture = null)
        {
            if (culture == null)
                return Core.Languages.Resource.ResourceManager.GetObject(resource, Core.Languages.Resource.Culture) as string;

            var cultureInfo = new CultureInfo(culture);
            return Core.Languages.Resource.ResourceManager.GetObject(resource, cultureInfo) as string;
        }

        #endregion Public Methods
    }
}