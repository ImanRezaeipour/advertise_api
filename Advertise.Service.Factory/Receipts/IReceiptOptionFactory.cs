using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Receipts;

namespace Advertise.Service.Factory.Receipts
{
    public interface IReceiptOptionFactory
    {
        Task<ReceiptOptionListModel> PrepareListModel(ReceiptOptionSearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}