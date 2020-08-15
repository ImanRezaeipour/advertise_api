using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Plans;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Plans;

namespace Advertise.Service.Plans
{
    public interface IPlanService
    {
        Task<int> CountByRequestAsync(PlanSearchModel model);
        Task CreateByViewModelAsync(PlanCreateModel model);
        Task DeleteByIdAsync(Guid? planId);
        Task EditByViewModelAsync(PlanEditModel model);
        Task<Plan> FindByCodeAsync(string code);
        Task<Plan> FindByIdAsync(Guid id);
        Task<IList<SelectList>> GetAllAsSelectListItemAsync();
        Task<IList<Plan>> GetByRequestAsync(PlanSearchModel model);
        IQueryable<Plan> QueryByRequest(PlanSearchModel model);
    }
}