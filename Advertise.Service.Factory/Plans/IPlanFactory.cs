using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Plans;

namespace Advertise.Service.Factory.Plans
{
    public interface IPlanFactory
    {
        Task<PlanCreateModel> PrepareCreateModelAsync(PlanCreateModel modelPrepare = null);
        Task<PlanEditModel> PrepareEditModelAsync(Guid id, PlanEditModel modelPrepare= null);
        Task<PlanListModel> PrepareListModelAsync(PlanSearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}