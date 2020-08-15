using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Receipts;

namespace Advertise.Service.Factory.Receipts
{
    public interface IReceiptFactory
    {
        Task<ReceiptDetailModel> PrepareDetailModelAsync(Guid receiptId);
        Task<ReceiptModel> PrepareCreateModelAsync(ReceiptModel modelPrepare = null);
        Task<ReceiptEditModel> PrepareEditModelAsync(Guid receiptId);
        Task<ReceiptListModel> PrepareListModelAsync(ReceiptSearchModel model, bool isCurrentUser = false, Guid? userId = null);
        Task<ReceiptPreviewModel> PreparePreviewModelAsync(Guid? id= null);
        Task<ReceiptModel> PrepareModelAsync();
    }
}