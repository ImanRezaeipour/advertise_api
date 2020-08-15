using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Reports;
using Advertise.Core.Managers.Kendo.DynamicLinq;
using Advertise.Core.Model.Reports;
using Stimulsoft.Report;

namespace Advertise.Service.Reports
{
    public interface IReportService
    {
        Task<StiReport> GetReportAsStiReportAsync(Guid reportId, ReportParameterModel model);
        Task<Report> FindByIdAsync(Guid reportId);
        IQueryable<Report> QueryByRequest(ReportSearchModel model);
        Task CreateByViewModelAsync(ReportCreateModel model);
        Task EditByViewModelAsync(ReportEditModel model);
        Task DeleteByIdAsync(Guid reportId);
        Task<DataSourceResult> ListByRequestAsync(DataSourceRequest request);
        Task<IList<Report>> GetByRequestAsync(ReportSearchModel model);
        Task<int> CountByRequestAsync(ReportSearchModel model);
        Task<IList<ReportModel>> GeAllAsync();
    }
}