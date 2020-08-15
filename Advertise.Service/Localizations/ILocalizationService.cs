namespace Advertise.Service.Localizations
{
    public interface ILocalizationService
    {
        string L(string resource, string culture = null);
        string Lang(string resource, string culture = null);
    }
}
