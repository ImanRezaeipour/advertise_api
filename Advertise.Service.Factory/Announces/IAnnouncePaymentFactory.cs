using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Announces;

namespace Advertise.Service.Factory.Announces
{
    public interface IAnnouncePaymentFactory
    {
        Task<AnnouncePaymentListModel> PrepareListModel(AnnouncePaymentSearchModel request, bool isCurrentUser = false, Guid? userId = null);
    }
}