using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Notices
{
    public class NoticeSearchModel : BaseSearchModel
    {
        public Guid? CreatedById { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}