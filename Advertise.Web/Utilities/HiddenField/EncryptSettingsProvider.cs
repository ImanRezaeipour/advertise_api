using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Advertise.Web.Utilities.HiddenField
{
    public class EncryptSettingsProvider : IEncryptSettingsProvider
    {
        public EncryptSettingsProvider()
        {
            //read settings from configuration
            var useHashingString = ConfigurationManager.AppSettings["UseHashingForEncryption"];
            var useHashing = string.Compare(useHashingString, "false", StringComparison.OrdinalIgnoreCase) != 0;

            EncryptionPrefix = ConfigurationManager.AppSettings["EncryptionPrefix"];
            if (string.IsNullOrWhiteSpace(EncryptionPrefix))
            {
                EncryptionPrefix = "encryptedHidden_";
            }

            var key = ConfigurationManager.AppSettings["EncryptionKey"];
            if (useHashing)
            {
                var hash = new SHA256Managed();
                EncryptionKey = hash.ComputeHash(Encoding.UTF8.GetBytes(key));
                hash.Clear();
                hash.Dispose();
            }
            else
            {
                EncryptionKey = Encoding.UTF8.GetBytes(key);
            }
        }

        #region ISettingsProvider Members

        public byte[] EncryptionKey { get; }

        public string EncryptionPrefix { get; }

        #endregion
    }
}