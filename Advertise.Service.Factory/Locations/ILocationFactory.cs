using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Locations;

namespace Advertise.Service.Factory.Locations
{
    public interface ILocationFactory
    {
        Task<LocationEditModel> PrepareEditModelAsync(Guid addressId);
        Task<LocationListModel> PrepareListModelAsync(LocationSearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}