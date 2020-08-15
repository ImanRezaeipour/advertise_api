using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Plans;
using Advertise.Core.Model.Plans;

namespace Advertise.Service.Plans
{
    public interface IPlanDiscountService
    {
        Task<int> CountByRequestAsync(PlanDiscountSearchModel model);
        Task CreateByViewModelAsync(PlanDiscountCreateModel model);
        Task DeleteByIdAsync(Guid? planDiscountId);
        Task EditByViewModelAsync(PlanDiscountEditModel model);
        Task<PlanDiscount> FindByIdAsync(Guid planDiscountId);
        Task<IList<PlanDiscount>> GetByRequestAsync(PlanDiscountSearchModel model);
        Task<int?> GetPercentByCodeAsync(string planDiscountCode);
        IQueryable<PlanDiscount> QueryByRequest(PlanDiscountSearchModel model);
    }
}