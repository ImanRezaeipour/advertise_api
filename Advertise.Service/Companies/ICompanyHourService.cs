using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Model.Companies;

namespace Advertise.Service.Companies
{
    public interface ICompanyHourService
    {
        Task<int> CountByRequestAsync(CompanyHourSearchModel model);
        Task CreateByViewModelAsync(CompanyHourCreateModel model);
        Task DeleteByIdAsync(Guid HourId);
        Task EditByViewModelAsync(CompanyHourEditModel model);
        Task<CompanyHour> FindAsync(Guid companyHourId);
        Task<CompanyHour> FindByUserIdAsync(Guid userId);
        IQueryable<CompanyHour> QueryByRequest(CompanyHourSearchModel model);
        Task<IList<CompanyHour>> GetByRequestAsync(CompanyHourSearchModel model);
        Task RemoveRangeAsync(IList<CompanyHour> companyHours);
        Task SeedAsync();
    }
}