using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Specifications;

namespace Advertise.Service.Factory.Specifications
{
    public interface ISpecificationOptionFactory
    {
        Task<SpecificationOptionCreateModel> PrepareCreateModelAsync();
        Task<SpecificationOptionDetailModel> PrepareDetailModelAsync(Guid specificationOptionId);
        Task<SpecificationOptionEditModel> PrepareEditModelAsync(Guid specificationOptionId);
        Task<SpecificationOptionListModel> PrepareListModelAsync(SpecificationOptionSearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}