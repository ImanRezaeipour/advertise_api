using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Plans;

namespace Advertise.Service.Factory.Plans
{
    public interface IPlanDiscountFactory
    {
        Task<PlanDiscountEditModel> PrepareEditModelAsync(Guid? id);
        Task<PlanDiscountListModel> PrepareListModelAsync(PlanDiscountSearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}