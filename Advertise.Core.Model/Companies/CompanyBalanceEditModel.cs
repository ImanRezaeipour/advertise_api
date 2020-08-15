using System;
using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyBalanceEditModel : BaseModel
    {
        public Guid Id { get; set; }
        public int? Amount { get; set; }
        public string AttachmentFile { get; set; }
        public Guid? CompanyId { get; set; }
        public IEnumerable<SelectList> CompanyList { get; set; }
        public string Depositor { get; set; }
        public string Description { get; set; }
        public string DocumentNumber { get; set; }
        public string IssueTracking { get; set; }
        public Guid? SettingTransactionId { get; set; }
        public IEnumerable<SelectList> SettingTransactionList { get; set; }
        public DateTime? TransactionedOn { get; set; }
    }
}