using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Model.Reports;
using Advertise.Service.Common;
using Advertise.Service.Reports;
using AutoMapper;

namespace Advertise.Service.Factory.Reports
{
    public class ReportFactory : IReportFactory
    {
        #region Private Fields

        private readonly ICommonService _commonService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;
        private readonly IReportService _reportService;

        #endregion Private Fields

        #region Public Constructors

        public ReportFactory(ICommonService commonService, IReportService reportService, IMapper mapper, IListService listService)
        {
            _commonService = commonService;
            _reportService = reportService;
            _mapper = mapper;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<ReportListModel> PrepareListModelAsync(ReportSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _reportService.CountByRequestAsync(model);
            var reports = await _reportService.GetByRequestAsync(model);
            var reportsViewModel = _mapper.Map<List<ReportModel>>(reports);
            var viewModel = new ReportListModel
            {
                Reports = reportsViewModel,
                SearchModel = model,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync()
            };

            return viewModel;
        }

        public async Task<ReportEditModel> PrepareEditModelAsync(Guid id, ReportEditModel modelPrepare = null)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            var report = await _reportService.FindByIdAsync(id);
            var viewModel = modelPrepare ?? _mapper.Map<ReportEditModel>(report);

            return viewModel;
        }

        public async Task<ReportParameterModel> PrepareReportParameter(Guid id)
        {
            var viewModel = new ReportParameterModel
            {
                Id = id
            };
            return viewModel;
        }

        #endregion Public Methods
    }
}