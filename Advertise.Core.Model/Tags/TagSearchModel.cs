using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Tags
{
    public class TagSearchModel : BaseSearchModel
    {
        public Guid? CreatedById { get; set; }
        public bool? IsActive { get; set; }
        public Guid? ProductId { get; set; }
    }
}