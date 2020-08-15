using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Manufacturers;

namespace Advertise.Service.Factory.Manufacturers
{
    public interface IManufacturerFactory
    {
        Task<ManufacturerEditModel> PrepareEditModelAsync(Guid id, ManufacturerEditModel modelPrepare = null);
        Task<ManufacturerCreateModel> PrepareCreateModelAsync(ManufacturerCreateModel modelPrepare = null);
        Task<ManufacturerListModel> PrepareListModelAsync(ManufacturerSearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}