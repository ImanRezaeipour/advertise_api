using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Products;
using Advertise.Core.Model.Products;

namespace Advertise.Service.Products
{
    public interface IProductVisitService
    {
        Task<int> CountAllByProductIdAsync(Guid productId);
        Task<int> CountByProductIdAsync(Guid productId);
        Task<int> CountByRequestAsync(ProductVisitSearchModel model);
        Task CreateAsync(ProductVisit productVisit);
        Task CreateByProductIdAsync(Guid productId);
        Task<ProductVisit> FindByIdAsync(Guid productVisitId);
        Task<ProductVisit> FindByProductIdAsync(Guid productId);
        Task<IList<ProductVisit>> GetByRequestAsync(ProductVisitSearchModel model);
        Task<List<Guid>> GetMostVisitProductIdAsync();
        Task<IList<Guid>> GetLastProductIdByCurrentUserAsync();
        Task<ProductVisitListModel> ListByRequestAsync(ProductVisitSearchModel model, bool isCurrentUser = false, Guid? userId = null);
        IQueryable<ProductVisit> QueryByRequest(ProductVisitSearchModel model);
        Task RemoveRangeAsync(IList<ProductVisit> productVisits);
        Task SeedAsync();
    }
}