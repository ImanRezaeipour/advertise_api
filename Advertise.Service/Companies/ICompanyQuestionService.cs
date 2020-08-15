using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Model.Companies;

namespace Advertise.Service.Companies
{
    public interface ICompanyQuestionService
    {
        Task ApproveByViewModelAsync(CompanyQuestionEditModel model);
        Task<int> CountByRequestAsync(CompanyQuestionSearchModel model);
        Task CreateByViewModelAsync(CompanyQuestionCreateModel model);
        Task DeleteByIdAsync(Guid companyQuestionId);
        Task EditByViewModelAsync(CompanyQuestionEditModel model);
        Task<CompanyQuestion> FindByIdAsync(Guid companyQuestionId);
        Task<IList<CompanyQuestion>> GetByCompanyIdAsync(Guid companyId);
        Task<IList<CompanyQuestion>> GetByRequestAsync(CompanyQuestionSearchModel model);
        Task<IList<CompanyQuestion>> GetAllByCompanyIdAsync(Guid companyId);
        IQueryable<CompanyQuestion> QueryByRequest(CompanyQuestionSearchModel model);
        Task RejectByViewModelAsync(CompanyQuestionEditModel model);
        Task RemoveRangeByCompanyId(Guid companyId);
    }
}