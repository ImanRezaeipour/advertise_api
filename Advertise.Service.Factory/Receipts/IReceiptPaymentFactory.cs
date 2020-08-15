using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Receipts;

namespace Advertise.Service.Factory.Receipts
{
    public interface IReceiptPaymentFactory
    {
        Task<ReceiptPaymentCompleteModel> PrepareCompleteModelAsync(string authority);
        Task<ReceiptPaymentListModel> PrepareListModelAsync(ReceiptPaymentSearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}