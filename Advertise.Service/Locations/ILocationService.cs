using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Locations;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Locations;

namespace Advertise.Service.Locations
{
    public interface ILocationService
    {
        Task CreateByViewModelAsync(LocationCreateModel model);
        Task DeleteByIdAsync(Guid addressId);
        Task EditByViewModelAsync(LocationEditModel model);
        Task SeedAsync();
        Task<int> CountByRequestAsync(LocationSearchModel model);
        Task<IList<Location>> GetByRequestAsync(LocationSearchModel model);
        Task<Location> FindDefaultAsync();
        Task<Location> FindByIdAsync(Guid addressId);
        Task<IList<SelectList>> GetProvinceAsSelectListItemAsync();
        IQueryable<Location> QueryByRequest(LocationSearchModel model);
        Task<Tuple<string, string,string>> GetDefaultLocationAsync();
    }
}