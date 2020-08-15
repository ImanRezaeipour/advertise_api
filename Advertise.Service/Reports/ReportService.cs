using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Configuration;
using Advertise.Core.Domain.Reports;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Kendo.DynamicLinq;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Reports;
using Advertise.Data.DbContexts;
using AutoMapper;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;

namespace Advertise.Service.Reports
{
    public class ReportService : IReportService
    {
        #region Private Fields

        private readonly IMapper _mapper;
        private readonly IDbSet<Report> _reportRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;
        private readonly IConfigurationManager _configurationManager;

        #endregion Private Fields

        #region Public Constructors

        public ReportService(IUnitOfWork unitOfWork, IMapper mapper, IWebContextManager webContextManager, IConfigurationManager configurationManager)
        {
            _unitOfWork = unitOfWork;
            _reportRepository = unitOfWork.Set<Report>();
            _mapper = mapper;
            _webContextManager = webContextManager;
            _configurationManager = configurationManager;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task CreateByViewModelAsync(ReportCreateModel model)
        {
            var report = _mapper.Map<Report>(model);
            if (model.ContentFile != null)
            {
                 var buffer = new byte[model.ContentFile.InputStream.Length];
                 model.ContentFile.InputStream.Read(buffer, 0, buffer.Length);
                 report.Content = Encoding.UTF8.GetString(buffer);
            }
           
            report.CreatedById = _webContextManager.CurrentUserId;
            _reportRepository.Add(report);

            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid reportId)
        {
            var report = await FindByIdAsync(reportId);
            _reportRepository.Remove(report);

            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task EditByViewModelAsync(ReportEditModel model)
        {
            var report = await FindByIdAsync(model.Id);
            _mapper.Map(model, report);

            await _unitOfWork.SaveAllChangesAsync();
        }
        
        public async Task<Report> FindByIdAsync(Guid reportId)
        {
            return await _reportRepository.SingleOrDefaultAsync(model => model.Id == reportId);
        }

        public async Task<StiReport> GetReportAsStiReportAsync(Guid reportId, ReportParameterModel model)
        {
            var report = await FindByIdAsync(reportId);
            var stiReport = new StiReport();
            var encoding = Encoding.UTF8;
            var docAsBytes = encoding.GetBytes(report.Content);
            stiReport.Load(docAsBytes);
            foreach (StiDatabase db in stiReport.Dictionary.Databases)
            {
                ((StiSqlDatabase)db).ConnectionString = _configurationManager.ConnectionString;
            }
            foreach (var item in model.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (item.GetValue(model) == null)
                    continue;

                if (item.FieldType == typeof(DateTime?))
                {
                    var value = ((DateTime)item.GetValue(model)).ToString("yyyy-MM-dd");
                    stiReport.Dictionary.Variables.Add(item.Name.GetNameViewModel(), value);
                }
                else
                    stiReport.Dictionary.Variables.Add(item.Name.GetNameViewModel(), item.GetValue(model));
            }
            return stiReport;
        }

        public async Task<DataSourceResult> ListByRequestAsync(DataSourceRequest request)
        {
            var result = _reportRepository.AsNoTracking().ToDataSourceResult(request);

            return new DataSourceResult
            {
                Data = _mapper.Map<List<ReportModel>>(result.Data),
                Total = result.Total,
                Aggregates = result.Aggregates
            };
        }

        public async Task<IList<ReportModel>> GeAllAsync()
        {
            var request = new ReportSearchModel();
            var reports = await  GetByRequestAsync(request);
            var dd =  _mapper.Map<List<ReportModel>>(reports);
            return dd;
        }

        public async Task<IList<Report>> GetByRequestAsync(ReportSearchModel model)
        {
            if(model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public async Task<int> CountByRequestAsync(ReportSearchModel model)
        {
            if(model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).CountAsync();
        }

        public IQueryable<Report> QueryByRequest(ReportSearchModel model)
        {
            var reports = _reportRepository.AsNoTracking().AsQueryable();

            if (model.CreatedById != null)
                reports = reports.Where(m => m.CreatedById == model.CreatedById);

            reports = reports.OrderBy($"{model.SortMember ?? SortMember.CreatedOn} {model.SortDirection ?? SortDirection.Desc}");

            return reports;
        }

        #endregion Public Methods
    }
}