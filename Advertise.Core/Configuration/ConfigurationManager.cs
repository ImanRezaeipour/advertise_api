using System;

namespace Advertise.Core.Configuration
{
    public class ConfigurationManager : IConfigurationManager
    {
        public string DisplayName => System.Configuration.ConfigurationManager.AppSettings["config:AdminDisplayName"];
        public string AdminEmail => System.Configuration.ConfigurationManager.AppSettings["config:AdminEmail"];
        public string AdminPassword => System.Configuration.ConfigurationManager.AppSettings["config:AdminPassword"];
        public string AdminUserName => System.Configuration.ConfigurationManager.AppSettings["config:AdminUserName"];
        public string AspNetIdentityRequiredEmail => System.Configuration.ConfigurationManager.AppSettings["config:AspNetIdentityRequiredEmail"];
        public string XsrfKey => System.Configuration.ConfigurationManager.AppSettings["config:XsrfKey"];
        public string PaymentDescription => System.Configuration.ConfigurationManager.AppSettings["config:PaymentDescription"];
        public string MerchantCode => System.Configuration.ConfigurationManager.AppSettings["config:MerchantCode"];
        public string PaymentCallbackUrl => System.Configuration.ConfigurationManager.AppSettings["config:PaymentCallbackUrl"];
        public string PlanPaymentCallbackUrl => System.Configuration.ConfigurationManager.AppSettings[ "config:PlanPaymentCallbackUrl"];
        public string BannerPaymentCallbackUrl => System.Configuration.ConfigurationManager.AppSettings[ "config:BannerPaymentCallbackUrl"];
        public string AdsPaymentCallbackUrl => System.Configuration.ConfigurationManager.AppSettings[ "config:AdsPaymentCallbackUrl"];
        public string ZarinpalGateway => System.Configuration.ConfigurationManager.AppSettings["config:ZarinpalGateway"];
        public string GoogleCientSecret => System.Configuration.ConfigurationManager.AppSettings["oauth:GoogleCientSecret"];
        public string GoogleClientId => System.Configuration.ConfigurationManager.AppSettings["oauth:GoogleClientId"];
        public string GoogleSystemEnable => System.Configuration.ConfigurationManager.AppSettings["oauth:GoogleSystemEnable"];
        public string RedisConnectionString => System.Configuration.ConfigurationManager.AppSettings["redis:ConnectionString"];
        public string RedisEnable => System.Configuration.ConfigurationManager.AppSettings["redis:Enable"];
        public string ConfirmationEmailUrl => System.Configuration.ConfigurationManager.AppSettings["url:ConfirmationEmail"];
        public string ConfirmationResetPasswordUrl => System.Configuration.ConfigurationManager.AppSettings["url:ConfirmationResetPassword"];
        public string SmsEnabled => System.Configuration.ConfigurationManager.AppSettings["sms:Enabled"];
        public string SmsUserApiKey => System.Configuration.ConfigurationManager.AppSettings["sms:UserApiKey"];
        public string SmsSecretKey => System.Configuration.ConfigurationManager.AppSettings["sms:SecretKey"];
        public string SmsLineNumber => System.Configuration.ConfigurationManager.AppSettings["sms:LineNumber"];
        public string VideoWaterMark => System.Configuration.ConfigurationManager.AppSettings["video:WaterMark"];
        public bool BundleEnabled => Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["bundle:Enabled"]);
        public bool CookieIsLocalhost => Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["cookie:IsLocalhost"]);
        public bool ExportImportUseDropdownlistsForAssociatedEntities => true;
        public string ConnectionString => System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationConnection"].ConnectionString;
    }
}
