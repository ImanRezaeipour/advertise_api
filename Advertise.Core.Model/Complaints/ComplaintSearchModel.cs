using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Complaints
{
    public class ComplaintSearchModel : BaseSearchModel
    {
        public Guid? CreatedById { get; set; }
        public bool? IsActive { get; set; }
    }
}