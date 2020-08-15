using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Seos;
using Advertise.Core.Model.Seos;

namespace Advertise.Service.Seos
{
    public interface ISeoUrlService
    {
        Task<int> CountByRequestAsync(SeoUrlSearchModel model);
        Task CreateByViewModelAsync(SeoUrlCreateModel model);
        Task DeleteByIdAsync(Guid id);
        Task EditByViewModelAsync(SeoUrlEditModel model);
        Task<SeoUrl> FindByIdAsync(Guid id);
        Dictionary<string, string> GetAll();
        Task<IList<SeoUrl>> GetByRequestAsync(SeoUrlSearchModel model);
        IQueryable<SeoUrl> QueryByRequest(SeoUrlSearchModel model);
    }
}