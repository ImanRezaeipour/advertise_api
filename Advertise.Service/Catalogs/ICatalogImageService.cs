using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Catalogs;
using Advertise.Core.Model.Catalogs;

namespace Advertise.Service.Catalogs
{
    public interface ICatalogImageService
    {
        Task<IList<CatalogImage>> GetByCatalogIdAsync(Guid catalogId);
        Task<IList<CatalogImage>> GetByRequestAsync(CatalogImageSearchModel request);
        IQueryable<CatalogImage> QueryByRequest(CatalogImageSearchModel request);
    }
}