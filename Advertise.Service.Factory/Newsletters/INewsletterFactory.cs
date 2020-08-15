using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Newsletters;

namespace Advertise.Service.Factory.Newsletters
{
    public interface INewsletterFactory
    {
        Task<NewsletterCreateModel> PrepareCreateModelAsync(NewsletterCreateModel modelPrepare= null);
        Task<NewsletterListModel> PrepareListModelAsync(NewsletterSearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}