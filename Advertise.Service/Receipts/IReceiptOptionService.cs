using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Receipts;
using Advertise.Core.Model.Receipts;

namespace Advertise.Service.Receipts
{
    public interface IReceiptOptionService
    {
        Task<int> CountByRequestAsync(ReceiptOptionSearchModel model);
        Task<IList<ReceiptOption>> GetByRequestAsync(ReceiptOptionSearchModel model);
        Task<IList<ReceiptOption>> GetMyReceiptOptionsByReceiptIdAsync(Guid receiptId, Guid? userId = null);
        decimal? GetSumTotalPriceAsync(Guid userId);
        IQueryable<ReceiptOption> QueryByRequest(ReceiptOptionSearchModel model);
    }
}