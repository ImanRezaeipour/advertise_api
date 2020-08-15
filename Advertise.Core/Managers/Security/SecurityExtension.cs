using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Advertise.Core.Managers.Security
{
    public static class SecurityExtension
    {
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Assert, Unrestricted = true)]
        public static string Encrypt(this string stringToEncrypt, string key)
        {
            if (string.IsNullOrEmpty(stringToEncrypt))
            {
                throw new ArgumentException("An empty string value cannot be encrypted.");
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Cannot encrypt using an empty key. Please supply an encryption key.");
            }
            
            var cspp = new CspParameters
            {
                KeyContainerName = key,
                Flags = CspProviderFlags.UseMachineKeyStore,
                //ProviderName = "Microsoft Strong Cryptographic Provider",
                //ProviderType = ((Environment.OSVersion.Version.Major > 5) || ((Environment.OSVersion.Version.Major == 5) && (Environment.OSVersion.Version.Minor >= 1))) ? 0x18 : 1
            };

            //var rule = new CryptoKeyAccessRule("everyone", CryptoKeyRights.FullControl, AccessControlType.Allow);
            //cspp.CryptoKeySecurity = new CryptoKeySecurity();
            //cspp.CryptoKeySecurity.SetAccessRule(rule);

            var rsa = new RSACryptoServiceProvider(cspp)
            {
                PersistKeyInCsp = true
            };

            byte[] bytes = rsa.Encrypt(System.Text.Encoding.UTF8.GetBytes(stringToEncrypt), true);

            return BitConverter.ToString(bytes);
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Assert, Unrestricted = true)]
        public static string Decrypt(this string stringToDecrypt, string key)
        {
            if (string.IsNullOrEmpty(stringToDecrypt))
            {
                throw new ArgumentException("An empty string value cannot be encrypted.");
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Cannot decrypt using an empty key. Please supply a decryption key.");
            }

            var cspp = new CspParameters
            {
                KeyContainerName = key,
                Flags = CspProviderFlags.UseMachineKeyStore,
                //ProviderName = "Microsoft Strong Cryptographic Provider",
                //ProviderType = ((Environment.OSVersion.Version.Major > 5) || ((Environment.OSVersion.Version.Major == 5) && (Environment.OSVersion.Version.Minor >= 1))) ? 0x18 : 1
            };

            //var rule = new CryptoKeyAccessRule("everyone", CryptoKeyRights.FullControl, AccessControlType.Allow);
            //cspp.CryptoKeySecurity = new CryptoKeySecurity();
            //cspp.CryptoKeySecurity.SetAccessRule(rule);

            var rsa = new RSACryptoServiceProvider(cspp)
            {
                PersistKeyInCsp = true
            };

            var decryptArray = stringToDecrypt.Split(new[] { "-" }, StringSplitOptions.None);
            var decryptByteArray = Array.ConvertAll(decryptArray, (s => Convert.ToByte(byte.Parse(s, System.Globalization.NumberStyles.HexNumber))));

            byte[] bytes = rsa.Decrypt(decryptByteArray, true);

            string result = System.Text.Encoding.UTF8.GetString(bytes);

            return result;
        }

        public static string GetSHA1Hash(this string stringToHash)
        {
            if (string.IsNullOrEmpty(stringToHash))
            {
                throw new ArgumentException("An empty string value cannot be hashed.");
            }

            Byte[] data = System.Text.Encoding.UTF8.GetBytes(stringToHash);
            Byte[] hash = new SHA1CryptoServiceProvider().ComputeHash(data);
            return Convert.ToBase64String(hash);
        }

        public static bool IsStrongPassword(this string s)
        {
            bool isStrong = Regex.IsMatch(s, @"[\d]");
            if (isStrong) isStrong = Regex.IsMatch(s, @"[a-z]");
            if (isStrong) isStrong = Regex.IsMatch(s, @"[A-Z]");
            if (isStrong) isStrong = Regex.IsMatch(s, @"[\s~!@#\$%\^&\*\(\)\{\}\|\[\]\\:;'?,.`+=<>\/]");
            if (isStrong) isStrong = s.Length > 7;
            return isStrong;
        }
    }
}