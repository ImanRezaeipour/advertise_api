using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyAttachmentFileModel : BaseModel
    {
        public string FileName { get; set; }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string FileSize { get; set; }
        public string FileExtension { get; set; }
        public string FileIcon
        {
            get
            {
                switch (FileExtension)
                {
                    case "pdf":
                        return "Admin.FileConst_PdfIcon";
                    case "rar":
                        return "Admin.FileConst_RarIcon";
                    case "zip":
                        return "Admin.FileConst_ZipIcon";
                    default:
                        return "Admin.FileConst_FileIcon";
                }
            }
            set { }
        }
    }
}