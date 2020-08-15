using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Notices;
using Advertise.Core.Model.Notices;

namespace Advertise.Service.Notices
{
    public interface INoticeService 
    {
        Task CreateByViewModelAsync(NoticeCreateModel model);
        Task DeleteByIdAsync(Guid newsId);
        Task<Notice> FindByIdAsync(Guid newsId);
        Task SeedAsync();
        Task EditByViewModelAsync(NoticeEditModel model);
        Task<IList<Notice>> GetByRequestAsync(NoticeSearchModel model);
        Task<int> CountByRequestAsync(NoticeSearchModel model);
        IQueryable<Notice> QueryByRequest(NoticeSearchModel model);
    }
}