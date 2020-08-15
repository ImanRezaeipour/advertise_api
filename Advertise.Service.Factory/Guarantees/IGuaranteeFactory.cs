using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Guarantees;

namespace Advertise.Service.Factory.Guarantees
{
    public interface IGuaranteeFactory
    {
        Task<GuaranteeEditModel> PrepareEditModelAsync(Guid id, GuaranteeEditModel modelPrepare = null);
        Task<GuaranteeListModel> PrepareListModelAsync(GuaranteeSearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}