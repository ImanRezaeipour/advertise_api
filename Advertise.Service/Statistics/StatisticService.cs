using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Statistics;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Statistics;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Statistics
{
    public class StatisticService : IStatisticService
    {
        #region Private Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IDbSet<Statistic> _statisticRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public StatisticService(IMapper mapper, IUnitOfWork unitOfWork, IEventPublisher eventPublisher)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _eventPublisher = eventPublisher;
            _statisticRepository = unitOfWork.Set<Statistic>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountAllAsync()
        {
            var searchRequest = new StatisticSearchModel
            {
                PageSize = PageSize.All,
                SortDirection = SortDirection.Asc,
                SortMember = SortMember.CreatedOn,
                PageIndex = 1
            };
            return  await CountByRequestAsync(searchRequest);
        }

        public async Task<int> CountByRequestAsync(StatisticSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return  await QueryByRequest(model).CountAsync();
        }

        public void CreateByViewModel(StatisticCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var statistic = _mapper.Map<Statistic>(model);
            _statisticRepository.Add(statistic);

            _unitOfWork.SaveAllChanges();
            _eventPublisher.EntityInserted(statistic);
        }

        public async Task DeleteByIdAsync(Guid statisticId)
        {
            if (statisticId == null)
                throw new ArgumentNullException(nameof(statisticId));

            var statistic = await _statisticRepository.FirstOrDefaultAsync(model => model.Id == statisticId);
            _statisticRepository.Remove(statistic);

            await _unitOfWork.SaveAllChangesAsync();
            _eventPublisher.EntityDeleted(statistic);
        }

        public async Task EditByViewModelAsync(StatisticEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var statistic = await _statisticRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            _mapper.Map(model, statistic);

            await _unitOfWork.SaveAllChangesAsync();
            _eventPublisher.EntityUpdated(statistic);
        }

        public async Task<Statistic> FindByIdAsync(Guid statisticId)
        {
            return  await _statisticRepository.FirstOrDefaultAsync(model => model.Id == statisticId);
        }

        public async Task<IList<Statistic>> GetByRequestAsync(StatisticSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public IQueryable<Statistic> QueryByRequest(StatisticSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var statistics = _statisticRepository.AsNoTracking().AsQueryable();
            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;

            statistics = statistics.OrderBy($"{model.SortMember} {model.SortDirection}");

            return statistics;
        }

        public async Task SeedAsync()
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods
    }
}