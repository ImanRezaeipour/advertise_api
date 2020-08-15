using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Catalogs;
using Advertise.Core.Model.Catalogs;

namespace Advertise.Service.Catalogs
{
    public interface ICatalogFeatureService
    {
        Task<IList<CatalogFeature>> GetByRequestAsync(CatalogFeatureSearchModel request);
        IQueryable<CatalogFeature> QueryByRequest(CatalogFeatureSearchModel request);
    }
}