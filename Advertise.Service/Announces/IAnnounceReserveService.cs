using System;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Announces;
using Advertise.Core.Model.Announces;

namespace Advertise.Service.Announces
{
    public interface IAnnounceReserveService
    {
        Task<int> CountByRequestAsync(AnnounceReserveSearchModel request);
        Task CreateByViewModelAsync(AnnounceReserveCreateModel viewModel);
        Task<DateTime?> GetReserveDayByOptionIdAsync(Guid optionId);
        IQueryable<AnnounceReserve> QueryByRequest(AnnounceReserveSearchModel request);
    }
}