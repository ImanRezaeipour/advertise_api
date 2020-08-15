using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Locations;
using Advertise.Core.Domain.Receipts;
using Advertise.Core.Model.Locations;
using Advertise.Core.Model.Receipts;

namespace Advertise.Service.Receipts
{
    public interface IReceiptService
    {
        Task<bool> HasByCurrentUserAsync();
        Task<int> CountByRequestAsync(ReceiptSearchModel model);
        Task CreateByViewModel(ReceiptModel model);
        Task DeleteByIdAsync(Guid receiptId);
        Task EditAddressByIdAsync(Guid receiptId, Location address);
        Task EditByViewModelAsync(ReceiptModel model);
        Task FinalUpdateByViewModel(ReceiptModel model);
        Task<Receipt> FindByIdAsync(Guid receiptId);
        Task<Receipt> FindByUserIdAsync(Guid userId, bool? isBuy = null);
        Task<Receipt> FindLastAddressByUserIdAsync(Guid userId);
        Task<string> GenerateCodeForReceiptAsync();
        Task<Location> GetAddressByUserId(Guid userId);
        Task<LocationModel> GetAddressViewModelAsync(Guid receiptId);
        Task<IList<Receipt>> GetByRequestAsync(ReceiptSearchModel model);
        Task<bool> IsExistByUserIdAsync(Guid userId, bool? isBuy = null);
        Task<string> MaxCodeByRequestAsync(ReceiptSearchModel model, string aggregateMember);
        IQueryable<Receipt> QueryByRequest(ReceiptSearchModel model);
        Task SetInvoiceNumberAsync(Guid receiptId, string invoiceNumber);
        Task SetIsBuyByReceiptIdAsync(Guid receiptId, bool isBuy);
    }
}