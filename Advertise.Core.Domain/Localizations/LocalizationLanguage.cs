using Advertise.Core.Domain.Common;

namespace Advertise.Core.Domain.Localizations
{
    public class LocalizationLanguage : BaseEntity
    {
        public virtual string Name { get; set; }
        public virtual string LanguageCulture { get; set; }
        public virtual string UniqueSeoCode { get; set; }
        public virtual string FlagImageFileName { get; set; }
        public virtual bool Rtl { get; set; }
        public virtual bool LimitedToStores { get; set; }
        public virtual int DefaultCurrencyId { get; set; }
        public virtual bool Published { get; set; }
        public virtual int DisplayOrder { get; set; }
    }
}
