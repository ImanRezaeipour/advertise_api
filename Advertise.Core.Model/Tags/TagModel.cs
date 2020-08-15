using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Tags
{
    public class TagModel : BaseModel
    {
        public string Code { get; set; }
        public string Color { get; set; }
        public string CostValue { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Description { get; set; }
        public string DurationDay { get; set; }
        public Guid Id { get; set; }
        public string LogoFileName { get; set; }
        public string Title { get; set; }
    }
}