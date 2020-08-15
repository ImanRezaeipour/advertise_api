using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Model.Companies;

namespace Advertise.Service.Companies
{
    public interface ICompanyVisitService
    {
        Task<int> CountByCompanyIdAsync(Guid companyId);
        Task<int> CountByRequestAsync(CompanyVisitSearchModel model);
        Task  CreateByCompanyIdAsync(Guid companyId);
        Task<CompanyVisit> FindAsync(Guid companyVisitId);
        Task<CompanyVisit> FindByCompanyIdAsync(Guid companyId);
        IQueryable<CompanyVisit> QueryByRequest(CompanyVisitSearchModel model);
        Task<IList<CompanyVisit>> GetByRequestAsync(CompanyVisitSearchModel model);
        Task<CompanyVisitListModel> ListByRequestAsync(CompanyVisitSearchModel model, bool isCurrentUser = false, Guid? userId = null);
        Task  SeedAsync();
    }
}