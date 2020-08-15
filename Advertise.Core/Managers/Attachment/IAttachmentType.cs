namespace Advertise.Core.Managers.Attachment
{
    public interface IAttachmentType
    {
        string MimeType
        {
            get;
        }

        string FriendlyName
        {
            get;
        }

        string Extension
        {
            get;
        }
    }

}
