using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Announces;
using Advertise.Core.Model.Announces;
using Advertise.Service.Common;

namespace Advertise.Service.Announces
{
    public interface IAnnouncePaymentService
    {
        Task<PaymentResult> CallbackWithZarinpalByViewModelAsync(AnnouncePaymentCallbackModel viewModel);
        Task<int> CountByRequestAsync(AnnouncePaymentSearchModel request);
        Task<PaymentResult> CreateWithZarinpalByViewModelAsync(AnnouncePaymentCreateModel viewModel);
        Task<AnnouncePayment> FindByAuthorityCodeAsync(string authorityCode);
        Task<IList<AnnouncePayment>> GetByRequestAsync(AnnouncePaymentSearchModel request);
        IQueryable<AnnouncePayment> QueryByRequest(AnnouncePaymentSearchModel request);
    }
}