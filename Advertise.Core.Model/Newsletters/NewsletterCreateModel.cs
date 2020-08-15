using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Newsletters
{
    public class NewsletterCreateModel : BaseModel
    {
        public string Email { get; set; }
        public MeetType Meet { get; set; }
        public IEnumerable<SelectList> MeetList { get; set; }
    }
}