using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Announces;
using Advertise.Core.Model.Announces;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Service.Announces
{
    public interface IAnnounceOptionService
    {
        Task<int> CountByRequestAsync(AnnounceOptionSearchModel request);
        Task CreateByModelAsync(AnnounceOptionCreateModel model);
        Task DeleteByIdAsync(Guid announceOptionId);
        Task EditByViewModelAsync(AnnounceOptionEditModel viewModel);
        Task<AnnounceOption> FindByIdAsync(Guid adsOptionId);
        Task<IList<SelectList>> GetAsSelectListAsync(AnnounceType type, AnnouncePositionType? position = null);
        Task<IList<AnnounceOption>> GetByRequestAsync(AnnounceOptionSearchModel request);
        Task<int> GetCapacityByIdAsync(Guid optionId);
        Task<decimal> GetPriceByIdAsync(Guid optionId, DurationType durationType);
        IQueryable<AnnounceOption> QueryByRequest(AnnounceOptionSearchModel request);
    }
}