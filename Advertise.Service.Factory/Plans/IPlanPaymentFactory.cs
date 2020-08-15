using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Plans;

namespace Advertise.Service.Factory.Plans
{
    public interface IPlanPaymentFactory
    {
        Task<PlanPaymentListModel> PrepareListModel(PlanPaymentSearchModel model, bool isCurrentUser = false, Guid? userId = null);
        Task<PlanPyamentCreateModel> PrepareCreateModel();
    }
}