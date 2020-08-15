using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Keywords;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Keywords;

namespace Advertise.Service.Keywords
{
    public interface IKeywordService
    {
        Task CreateByViewModelAsync(KeywordCreateModel model);
        Task DeleteByIdAsync(Guid keywordId);
        Task<Keyword> FindByIdAsync(Guid keywordId, bool? applyCurrentUser = false);
        Task<List<Keyword>> GetAllActiveAsync();
        IQueryable<Keyword> QueryByRequest(KeywordSearchModel model);
        Task<List<SelectList>> GetAllActiveAsSelectListAsync();
        Task<bool> IsExistByIdAsync(Guid keywordId);
    }
}