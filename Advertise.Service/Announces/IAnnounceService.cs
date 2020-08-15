using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Announces;
using Advertise.Core.Model.Announces;
using Advertise.Core.Objects;
using Advertise.Service.Common;

namespace Advertise.Service.Announces
{
    public interface IAnnounceService
    {
        Task ApproveByIdAsync(Guid adsId);
        Task<int> CountByRequestAsync(AnnounceSearchModel request);
        Task<PaymentResult> CreateByViewModelAsync(AnnounceCreateModel viewModel, bool? isFreeOfCharge = null);
        Task EditByViewModelAsync(AnnounceEditModel viewModel);
        Task<Core.Domain.Announces.Announce> FindByIdAsync(Guid bannerId);
        Task<IList<Announce>> GetByRequestAsync(AnnounceSearchModel request);
        Task<IList<FineUploaderObject>> GetFileAsFineUploaderModelByIdAsync(Guid categoryId);
        IQueryable<Core.Domain.Announces.Announce> QueryByRequest(AnnounceSearchModel request);
        Task RejectByIdAsync(Guid adsId);
    }
}