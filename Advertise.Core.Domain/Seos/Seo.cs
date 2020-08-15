using Advertise.Core.Domain.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Domain.Seos
{
    public class Seo : BaseEntity
    {
        public string EntityId { get; set; }
        public string EntityName { get; set; }
        public bool IsActive { get; set; }
        public LanguageType? Language { get; set; }
        public string Slug { get; set; }
    }
}