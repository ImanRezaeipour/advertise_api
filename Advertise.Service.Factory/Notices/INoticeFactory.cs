using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Notices;

namespace Advertise.Service.Factory.Notices
{
    public interface INoticeFactory
    {
        Task<NoticeEditModel> PrepareEditModelAsync(Guid newsId);
        Task<NoticeListModel> PrepareListModelAsync(NoticeSearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}