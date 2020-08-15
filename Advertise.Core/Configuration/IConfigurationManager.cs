namespace Advertise.Core.Configuration
{
    public interface IConfigurationManager
    {
        string DisplayName { get; }
        string AdminEmail { get; }
        string AdminPassword { get; }
        string AdminUserName { get; }
        string AspNetIdentityRequiredEmail { get; }
        string XsrfKey { get; }
        string PaymentDescription { get; }
        string MerchantCode { get; }
        string PaymentCallbackUrl { get; }
        string ZarinpalGateway { get; }
        string RedisConnectionString { get; }
        string GoogleCientSecret { get; }
        string GoogleClientId { get; }
        string GoogleSystemEnable { get; }
        string PlanPaymentCallbackUrl { get; }
        string ConfirmationEmailUrl { get; }
        string ConfirmationResetPasswordUrl { get; }
        string SmsUserApiKey { get; }
        string SmsSecretKey { get; }
        string SmsLineNumber { get; }
        string SmsEnabled { get; }
        string VideoWaterMark { get; }
        bool BundleEnabled { get; }
        string BannerPaymentCallbackUrl { get; }
        string AdsPaymentCallbackUrl { get; }
        bool CookieIsLocalhost { get; }
        bool ExportImportUseDropdownlistsForAssociatedEntities { get; }
        string ConnectionString { get; }
    }
}