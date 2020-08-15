using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Model.Statistics;
using Advertise.Service.Common;
using Advertise.Service.Statistics;
using AutoMapper;

namespace Advertise.Service.Factory.Statistics
{
    public class StatisticFactory : IStatisticFactory
    {
        #region Private Fields

        private readonly ICommonService _commonService;
        private readonly IListService _listService;
        private readonly IStatisticService _statisticService;
        private readonly IMapper _mapper;

        #endregion Private Fields

        #region Public Constructors

        public StatisticFactory(ICommonService commonService, IStatisticService statisticService, IMapper mapper, IListService listService)
        {
            _commonService = commonService;
            _statisticService = statisticService;
            _mapper = mapper;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<StatisticListViewModel> PrepareListModelAsync(StatisticSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _statisticService.CountByRequestAsync(model);
            var statistic = await _statisticService.GetByRequestAsync(model);
            var statisticViewModel = _mapper.Map<IList<StatisticModel>>(statistic);
            var viewModel = new StatisticListViewModel
            {
                SearchModel = model,
                Statistics = statisticViewModel,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync()
            };

            return viewModel;
        }

        #endregion Public Methods
    }
}