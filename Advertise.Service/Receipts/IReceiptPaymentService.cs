using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Receipts;
using Advertise.Core.Model.Receipts;
using Advertise.Service.Common;

namespace Advertise.Service.Receipts
{
    public interface IReceiptPaymentService
    {
        Task<ServiceResult> CallbackByViewModelAsync(ReceiptPaymentCallbackModel model);
        Task<int> CountByRequestAsync(ReceiptPaymentSearchModel model);
        Task<string> CreateAsync();
        Task<ReceiptPayment> FindByIdAsync(Guid paymentId);
        Task<ReceiptPayment> FindByAuthorityCodeAsync(string authorityCode);
        Task<IList<ReceiptPayment>> GetByRequestAsync(ReceiptPaymentSearchModel model);
        IQueryable<ReceiptPayment> QueryByRequest(ReceiptPaymentSearchModel model);
    }
}