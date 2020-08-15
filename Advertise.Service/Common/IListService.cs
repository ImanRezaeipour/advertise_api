using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Common;

namespace Advertise.Service.Common
{
    public interface IListService
    {
        Task<IList<SelectList>> GetIsBanListAsync();
        Task<IList<SelectList>> GetIsVerifyListAsync();
        Task<IList<SelectList>> GetPageSizeListAsync();
        Task<IList<SelectList>> GetSortDirectionFilterListAsync();
        Task<IList<SelectList>> GetSortDirectionListAsync();
        Task<IList<SelectList>> GetSortMemberFilterListAsync();
        Task<IList<SelectList>> GetSortMemberListByTitleAsync();
    }
}