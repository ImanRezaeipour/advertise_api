namespace Advertise.Core.Managers.Attachment
{
    public sealed class AttachmentType : IAttachmentType
    {
        // Possibly make this private if you only use the static predefined MIME types.
        public AttachmentType(string mimeType, string friendlyName, string extension)
        {
            this.MimeType = mimeType;
            this.FriendlyName = friendlyName;
            this.Extension = extension;
        }

        public static IAttachmentType UnknownMime { get; } = new AttachmentType("application/octet-stream", "Unknown", string.Empty);

        public static IAttachmentType Photo { get; } = new AttachmentType("image/png", "Photo", ".jpg");

        public static IAttachmentType Video { get; } = new AttachmentType("video/mp4", "Video", ".mp4");

        public static IAttachmentType Document { get; } = new AttachmentType("application/pdf", "Document", ".pdf");

        public static IAttachmentType Unknown { get; } = new AttachmentType(string.Empty, "Unknown", string.Empty);

        public string MimeType { get; }

        public string FriendlyName { get; }

        public string Extension { get; }
    }
}
