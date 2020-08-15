using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Statistics;

namespace Advertise.Service.Factory.Statistics
{
    public interface IStatisticFactory
    {
        Task<StatisticListViewModel> PrepareListModelAsync(StatisticSearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}