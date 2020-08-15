using System;

namespace Advertise.Web.Utilities.HiddenField
{
    public interface IRijndaelStringEncrypter : IDisposable
    {
        string Encrypt(string value);

        string Decrypt(string value);
    }
}