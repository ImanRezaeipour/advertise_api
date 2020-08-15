using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Emails
{
    public class EmailSearchModel : BaseSearchModel
    {
        public Guid? CreatedById { get; set; }
    }
}