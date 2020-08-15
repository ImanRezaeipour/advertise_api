using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Companies;
using Advertise.Core.Types;

namespace Advertise.Service.Companies
{
    public interface ICompanyReviewService
    {
        Task<int> CountByRequestAsync(CompanyReviewSearchModel model);
        Task CreateByViewModelAsync(CompanyReviewCreateModel model);
        Task DeleteByIdAsync(Guid companyReviewId);
        Task ApproveByViewModelAsync(CompanyReviewEditModel model);
        Task EditByViewModelAsync(CompanyReviewEditModel model);
        Task<CompanyReview> EditAsync(CompanyReviewEditModel model);
        Task RejectByViewModelAsync(CompanyReviewEditModel model);
        Task<CompanyReview> FindByIdAsync(Guid companyReviewId);
        Task<IList<CompanyReview>> GetByRequestAsync(CompanyReviewSearchModel model);
        Task<IList<SelectList>> GetCompanyAsSelectListItemAsync();
        Task<IList<CompanyReview>> GetByCompanyIdAsync(Guid companyId,StateType? state= null);
        IQueryable<CompanyReview> QueryByRequest(CompanyReviewSearchModel model);
        Task RemoveRangeAsync(IList<CompanyReview> companyReviews);
        Task SeedAsync();
    }
}