using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Newsletters
{
    public class NewsletterSearchModel : BaseSearchModel
    {
        public Guid? CreatedById { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? Email { get; set; }
        public MeetType? Meet { get; set; }
    }
}