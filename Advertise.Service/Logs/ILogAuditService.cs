using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Logs;
using Advertise.Core.Model.Logs;

namespace Advertise.Service.Logs
{
    public interface ILogAuditService
    {
        Task<int> CountByRequestAsync(LogAuditSearchModel model);
        Task CreateByViewModelAsync(LogAuditCreateModel model);
        Task DeleteByIdAsync(Guid auditLogId);
        Task EditByViewModelAsync(LogAuditEditModel model);
        Task<LogAudit> FindByIdAsync(Guid auditLogId);
        Task<IList<LogAudit>> GetByRequestAsync(LogAuditSearchModel model);
        Task<LogAuditListModel> ListByRequestAsync(LogAuditSearchModel model);
        IQueryable<LogAudit> QueryByRequest(LogAuditSearchModel model);
        Task SeedAsync();
    }
}