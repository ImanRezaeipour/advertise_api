using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Newsletters
{
    public class NewsletterModel : BaseModel
    {
        public string Email { get; set; }
        public Guid Id { get; set; }
        public MeetType Meet { get; set; }
    }
}