using System;
using Advertise.Core.Domain.Common;

namespace Advertise.Core.Domain.Companies
{
   public class CompanyAttachmentFile : BaseAttachment
    {
        public virtual int? Order { get; set; }
        public virtual CompanyAttachment CompanyAttachment { get; set; }
        public virtual Guid? CompanyAttachmentId { get; set; }
    }
}
