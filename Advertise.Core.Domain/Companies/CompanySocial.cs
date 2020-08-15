using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Users;

namespace Advertise.Core.Domain.Companies
{
    public class CompanySocial : BaseEntity
    {
        public virtual string TwitterLink { get; set; }
        public virtual string FacebookLink { get; set; }
        public virtual string GooglePlusLink { get; set; }
        public virtual string YoutubeLink { get; set; }
        public virtual string InstagramLink { get; set; }
        public virtual string TelegramLink { get; set; }
        public virtual Company Company { get; set; }
        public virtual Guid? CompanyId { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
    }
}