using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Model.Companies;

namespace Advertise.Service.Companies
{
    public interface ICompanyTagService
    {
        Task<int> CountAllTagByCompanyIdAsync(Guid companyId);
        Task<int> CountByRequestAsync(CompanyTagSearchModel model);
        Task CreateByViewModelAsync(CompanyTagCreateModel model);
        Task  DeleteByIdAsync(Guid companyTagId);
        Task  EditByViewModelAsync(CompanyTagEditModel model);
        Task<CompanyTag> FindAsync(Guid companyTagId);
        IQueryable<CompanyTag> QueryByRequest(CompanyTagSearchModel model);
        Task<CompanyTagListModel> GetByCompanyIdAsync(Guid companyId);
        Task<IList<CompanyTag>> GetCompanyTagsByRequestAsync(CompanyTagSearchModel model);
        Task<CompanyTagListModel> ListByRequestAsync(CompanyTagSearchModel model);
        Task  SeedAsync();
    }
}