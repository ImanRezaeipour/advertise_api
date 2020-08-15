using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Manufacturers;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Manufacturers;

namespace Advertise.Service.Manufacturers
{
    public interface IManufacturerService
    {
        Task<IList<SelectList>> GetAllAsSelectListAsync();
        Task<Manufacturer> FindByIdAsync(Guid id);
        Task EditByViewMoodelAsync(ManufacturerEditModel model);
        Task CreateByViewModelAsync(ManufacturerCreateModel model);
        IQueryable<Manufacturer> QueryByRequest(ManufacturerSearchModel model);
        Task<int> CountByRequestAsync(ManufacturerSearchModel model);
        Task<IList<Manufacturer>> GetByRequestAsync(ManufacturerSearchModel model);
        Task DeleteByIdAsync(Guid id);
    }
}