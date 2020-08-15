using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Smses;

namespace Advertise.Service.Factory.Messages
{
    public interface ISmsFactory
    {
        Task<SmsListViewModel> PrepareListViewModelAsync(SmsSearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}