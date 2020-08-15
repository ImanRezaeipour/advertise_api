using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Reports;

namespace Advertise.Service.Factory.Reports
{
    public interface IReportFactory
    {
        Task<ReportListModel> PrepareListModelAsync(ReportSearchModel model, bool isCurrentUser = false, Guid? userId = null);
        Task<ReportEditModel> PrepareEditModelAsync(Guid id, ReportEditModel modelPrepare = null);
        Task<ReportParameterModel> PrepareReportParameter(Guid id);
    }
}