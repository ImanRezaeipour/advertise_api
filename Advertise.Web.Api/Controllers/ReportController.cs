using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Reports;
using Advertise.Service.Factory.Reports;
using Advertise.Service.Reports;
using Advertise.Web.Filters;
using Stimulsoft.Report.Mvc;

namespace Advertise.Web.Api.Controllers
{
    public class ReportController : BaseController
    {
        #region Private Fields

        private readonly IReportService _reportService;
        private readonly IReportFactory _reportFactory;

        #endregion Private Fields

        #region Public Constructors

        public ReportController(IReportService reportService, IReportFactory reportFactory)
        {
            _reportService = reportService;
            _reportFactory = reportFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPost]
        public virtual async Task<HttpResponseMessage> Create(ReportCreateModel viewModel)
        {
            await _reportService.CreateByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public virtual async Task<HttpResponseMessage> DeleteAjax(RequestModel model)
        {
            await _reportService.DeleteByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public virtual async Task<HttpResponseMessage> Detail(ReportParameterModel viewModel = null)
        {
            var options = new StiMvcViewerOptions();
            return Request.CreateResponse(HttpStatusCode.OK, options);
        }

        public virtual async Task<HttpResponseMessage> DetailExportReport(RequestModel model)
        {
            var report = StiMvcViewer.GetReportObject();
            //StiRequestParams parameters = StiMvcViewer.GetRequestParams();

            //if (parameters.ExportFormat == StiExportFormat.Pdf)
            //{
            //    // Some actions with report when exporting to PDF
            //}
            return Request.CreateResponse(HttpStatusCode.OK, StiMvcViewer.ExportReportResult(report));
        }

        public virtual async Task<HttpResponseMessage> DetailInteraction(RequestModel model)
        {
            //StiRequestParams requestParams = StiMvcViewer.GetRequestParams();
            //if (requestParams.Action == StiAction.Variables)
            //{
            //    DataSet data = new DataSet();
            //    data.ReadXml(Server.MapPath("~/Content/Data/Demo.xml"));

            //    StiReport report = StiMvcViewer.GetReportObject();
            //    report.RegData("Demo", data);

            //    return StiMvcViewer.InteractionResult(report);
            //}
            return Request.CreateResponse(HttpStatusCode.OK, StiMvcViewer.InteractionResult());
        }

        public virtual async Task<HttpResponseMessage> DetailPrintReport(RequestModel model)
        {
            var report = StiMvcViewer.GetReportObject();
            return Request.CreateResponse(HttpStatusCode.OK, StiMvcViewer.PrintReportResult(report));
        }

        public virtual async Task<HttpResponseMessage> DetailReport(ReportParameterModel viewModel)
        {
            var report = await _reportService.GetReportAsStiReportAsync(viewModel.Id ,viewModel);
            return Request.CreateResponse(HttpStatusCode.OK, StiMvcViewer.GetReportSnapshotResult(report));
        }

        [SkipRemoveWhiteSpaces]
        public virtual async Task<HttpResponseMessage> DetailViewerEvent(RequestModel model)
        {
            return Request.CreateResponse(HttpStatusCode.OK, StiMvcViewer.ViewerEventResult());
        }

        public virtual async Task<HttpResponseMessage> Edit(RequestModel model)
        {
            var viewModel = await _reportFactory.PrepareEditModelAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        public virtual async Task<HttpResponseMessage> Edit(ReportEditModel viewModel)
        {
            await _reportService.EditByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize]
        public virtual async Task<HttpResponseMessage> GetListAjax()
        {
            var list = await _reportService.GeAllAsync();
            return Request.CreateResponse(HttpStatusCode.OK, list);
        }

        public virtual async Task<HttpResponseMessage> List(ReportSearchModel request)
        {
            var viewModel = await _reportFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        public virtual async Task<HttpResponseMessage> FromDateToDateAjax(RequestModel model)
        {
            var viewModel = await _reportFactory.PrepareReportParameter(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        #endregion Public Methods
    }
}