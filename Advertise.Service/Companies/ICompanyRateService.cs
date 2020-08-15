using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Model.Companies;

namespace Advertise.Service.Companies
{
    public interface ICompanyRateService
    {
        Task<int> CountByRequestAsync(CompanyRateSearchModel model);
        Task CreateByViewModelAsync(CompanyRateModel model);
        IQueryable<CompanyRate> QueryByRequest(CompanyRateSearchModel model);
        Task<decimal> RateByRequestAsync(CompanyRateSearchModel model);
    }
}