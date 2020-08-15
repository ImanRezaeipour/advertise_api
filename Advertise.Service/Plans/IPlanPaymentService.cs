using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Plans;
using Advertise.Core.Model.Plans;
using Advertise.Service.Common;

namespace Advertise.Service.Plans
{
    public interface IPlanPaymentService
    {
        Task<PaymentResult> CallbackByViewModelAsync(PlanPaymentCallbackModel model);
        Task<PaymentResult> CallbackWithZarinpalByViewModelAsync(PlanPaymentCallbackModel model);
        Task<int> CountByRequestAsync(PlanPaymentSearchModel model);
        Task<PaymentResult> CreateByViewModelAsync(PlanPyamentCreateModel model);
        Task<PaymentResult> CreateWithZarinpalByViewModelAsync(PlanPyamentCreateModel model);
        Task<PlanPayment> FindByAuthorityCodeAsync(string authorityCode);
        Task<IList<PlanPayment>> GetByRequestAsync(PlanPaymentSearchModel model);
        IQueryable<PlanPayment> QueryByRequest(PlanPaymentSearchModel model);
    }
}