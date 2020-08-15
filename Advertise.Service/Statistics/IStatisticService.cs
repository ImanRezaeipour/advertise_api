using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Statistics;
using Advertise.Core.Model.Statistics;

namespace Advertise.Service.Statistics
{
    public interface IStatisticService 
    {
        Task<int> CountAllAsync();
        Task DeleteByIdAsync(Guid statisticId);
        Task<Statistic> FindByIdAsync(Guid statisticId);
        Task SeedAsync();
        Task EditByViewModelAsync(StatisticEditModel model);
        Task<IList<Statistic>> GetByRequestAsync(StatisticSearchModel model);
        Task<int> CountByRequestAsync(StatisticSearchModel model);
        IQueryable<Statistic> QueryByRequest(StatisticSearchModel model);
        void CreateByViewModel(StatisticCreateModel model);
    }
}