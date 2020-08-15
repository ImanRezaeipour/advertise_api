using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Specifications;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Specifications;

namespace Advertise.Service.Specifications
{
    public interface ISpecificationOptionService
    {
        Task<int> CountByRequestAsync(SpecificationOptionSearchModel model);
        Task CreateByViewModelAsync(SpecificationOptionCreateModel model);
        Task DeleteByIdAsync(Guid specificationOptionId);
        Task EditByViewModelAsync(SpecificationOptionEditModel model);
        Task<SpecificationOption> FindByIdAsync(Guid specificationOptionId);
        Task<SpecificationOption> FindWithCategoryAsync(Guid specificationOptionId);
        Task<IList<SpecificationOption>> GetByRequestAsync(SpecificationOptionSearchModel model);
        Task<Guid?> GetIdByTitleAsync(string specificationOptionTitle, Guid specificationId);
        Guid? GetIdByTitle(string specificationOptionTitle, Guid specificationId);
        Task<IList<SpecificationOption>> GetSpecificationOptionsBySpecificationIdAsync(Guid specificationId);
        Task<IList<SelectList>> GetAsSelectListBySpecificationIdAsync(Guid specificationId);
        Task<IList<SpecificationOptionModel>> GetViewModelBySpecificationIdAsync(Guid specificationId);
        IQueryable<SpecificationOption> QueryByRequest(SpecificationOptionSearchModel model);
        Task SeedAsync();
    }
}