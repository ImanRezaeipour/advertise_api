using Advertise.Core.Domain.Common;

namespace Advertise.Core.Domain.Settings
{
   public class SettingTransaction:BaseEntity
    {
        public virtual string NameOfAccountNumber { get; set; }
        public virtual string CorporationShebaNumber { get; set; }
        public virtual string CorporationShetabNumber { get; set; }
        public virtual string CorporationAccountNumber { get; set; }
        public virtual string BankName { get; set; }
    }
}
