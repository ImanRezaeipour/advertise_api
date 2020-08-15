using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Specifications;

namespace Advertise.Service.Factory.Specifications
{
    public interface ISpecificationFactory
    {
        Task<SpecificationDetailModel> PrepareDetailModelAsync(Guid specificationId);
        Task<SpecificationCreateModel> PrepareCreateModelAsync(SpecificationCreateModel modelPrepare = null);
        Task<SpecificationEditModel> PrepareEditModelAsync(Guid specificationId, SpecificationEditModel modelPrepare = null);
        Task<SpecificationListModel> PrepareListViewModelAsync(SpecificationSearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}