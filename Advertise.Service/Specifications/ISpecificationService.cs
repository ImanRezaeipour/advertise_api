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
    public interface ISpecificationService
    {
        Task<int> CountByRequestAsync(SpecificationSearchModel model);
        Task CreateByViewModelAsync(SpecificationCreateModel model);
        Task DeleteByIdAsync(Guid specificationId);
        Task EditByViewModelAsync(SpecificationEditModel model);
        Task<Specification> FindByIdAsync(Guid specificationId);
        Task<IList<SelectList>> GetAsSelectListItemAsync(Guid categoryId);
        Task<IList<Specification>> GetByCategoryIdAsync(Guid categoryId);
        Task<IList<Specification>> GetByRequestAsync(SpecificationSearchModel model);
        Guid? GetIdByTitle(string specificationTitle, Guid categoryId);
        Task<List<string>> GetTitlesAsync();
        Task<object> GetObjectByCategoryAsync(Guid categoryId);
        Task<IList<SpecificationModel>> GetViewModelByCategoryAliasAsync(string categoryAlias);
        Task<IQueryable<Specification>> QueryByRequest(SpecificationSearchModel model);
    }
}