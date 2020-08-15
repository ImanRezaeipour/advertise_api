using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Locations;

namespace Advertise.Service.Factory.Locations
{
    public interface ILocationCityFactory
    {
        Task<LocationCityListModel> PrepareListModelAsync(LocationCitySearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}