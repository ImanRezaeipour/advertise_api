namespace Advertise.Web.Utilities.HiddenField
{
    public interface IEncryptSettingsProvider
    {
        byte[] EncryptionKey { get; }

        string EncryptionPrefix { get; }
    }
}