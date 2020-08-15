using System;
using Advertise.Core.Domain.Common;

namespace Advertise.Core.Domain.Localizations
{
    public class Localization : BaseEntity
    {
        public virtual Guid LocalizationLanguageId { get; set; }
        public virtual string ResourceName { get; set; }
        public virtual string ResourceValue { get; set; }
        public virtual LocalizationLanguage LocalizationLanguage { get; set; }
    }
}
