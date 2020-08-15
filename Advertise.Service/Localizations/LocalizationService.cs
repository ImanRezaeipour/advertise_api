namespace Advertise.Service.Localizations
{
    public class LocalizationService : ILocalizationService
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

        #endregion Public Methods
    }
}