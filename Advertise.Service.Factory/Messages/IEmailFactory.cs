using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Emails;

namespace Advertise.Service.Factory.Messages
{
    public interface IEmailFactory
    {
        Task<EmailListModel> PrepareListViewModelAsync(EmailSearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}