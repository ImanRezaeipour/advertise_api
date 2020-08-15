using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Catalogs;
using Advertise.Core.Model.Catalogs;

namespace Advertise.Service.Catalogs
{
    public interface ICatalogSpecificationService
    {
        Task<IList<CatalogSpecification>> GetByCatalogIdAsync(Guid catalogId);
        IQueryable<CatalogSpecification> QueryByRequest(CatalogSpecificationSearchModel request);
        Task<IList<CatalogSpecification>> GetByRequestAsync(CatalogSpecificationSearchModel request);
        Task<Guid?> GetCatalogIdBySpecificationId(Guid specificationId, Guid specificationOptionId);
    }
}