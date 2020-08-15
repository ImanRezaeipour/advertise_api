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
    public interface ILocationCityService
    {
        Task<int> CountByRequestAsync(LocationCitySearchModel model);
        Task  DeleteByIdAsync(Guid cityId);
        Task<LocationCity> FindByIdAsync(Guid id);
        Task<LocationCity> FindDefaultAsync();
        Task<IList<LocationCity>> GetByRequestAsync(LocationCitySearchModel model);
        Task<IList<SelectList>> GetCityAsSelectListItemAsync(Guid cityId);
        Task <object> GetStatesAsync();
        Task SeedAsync();
        IQueryable<LocationCity> QueryByRequest(LocationCitySearchModel model);
        Task<Guid?> GetIdByNameAsync(string cityName);
        Task<LocationCityModel> GetLocationAsync(Guid cityId);
        Task<string> GetNameByIdAsync(Guid cityId);
    }
}