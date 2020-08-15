namespace Advertise.Core.Managers.Attachment
{
    public class AttachmentManager:IAttachmentManager
    {
        public IAttachmentValidator AttachmentValidator { get; }

        public AttachmentManager(IAttachmentValidator attachmentValidator)
        {
            AttachmentValidator = attachmentValidator;
        }
    }
}
