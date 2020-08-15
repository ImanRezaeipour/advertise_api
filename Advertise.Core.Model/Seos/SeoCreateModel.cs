using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Seos
{
    public class SeoCreateModel : BaseModel
    {
        public string EntityId { get; set; }
        public string EntityName { get; set; }
        public bool IsActive { get; set; }
        public LanguageType? Language { get; set; }
        public string Slug { get; set; }
    }
}