using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Model.Companies;
using Advertise.Core.Objects;

namespace Advertise.Service.Companies
{
    public interface ICompanyBalanceService
    {
        Task<int> CountByRequestAsync(CompanyBalanceSearchModel model);
        Task CreateByViewModelAsync(CompanyBalanceCreateModel viewModel);
        Task DeleteByIdAsync(Guid companyBalanceId);
        Task EditByViewModelAsync(CompanyBalanceEditModel model);
        Task<CompanyBalance> FindByIdAsync(Guid companyBalanceId);
        Task<IList<CompanyBalance>> GetByRequestAsync(CompanyBalanceSearchModel model);
        Task<IList<FineUploaderObject>> GetFileAsFineUploaderModelByIdAsync(Guid companyBalanceId);
        IQueryable<CompanyBalance> QueryByRequest(CompanyBalanceSearchModel model);
    }
}