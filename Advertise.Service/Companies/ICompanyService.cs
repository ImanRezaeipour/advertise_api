using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Advertise.Core.Common;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Domain.Locations;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Companies;
using Advertise.Core.Model.Locations;
using Advertise.Core.Objects;
using Advertise.Core.Types;

namespace Advertise.Service.Companies
{
    public interface ICompanyService
    {
        Task<int> CountAllAsync();
        Task<bool> HasAliasAsync(Guid input, CancellationToken cancellationToken);
        Task<int> CountByRequestAsync(CompanySearchModel model);
        Task<int> CountByStateAsync(StateType companyState);
        Task<int> CountByCategoryIdAsync(Guid categoryId);
        Task CreateByUserIdAsync(Guid userId);
        Task CreateEasyByViewModelAsync(CompanyCreateModel model);
        Task DeleteByAliasAsync(string companyAlias, bool isCurrentUser = false);
        Task DeleteByUserIdAsync(Guid userId);
        Task EditApproveByViewModelAsync(CompanyEditModel model);
        Task EditByViewModelAsync(CompanyEditModel model, bool isCurrentUser = false);
        Task<bool> IsExistAliasCancellationTokenAsync(string alias,HttpContext http, CancellationToken cancellationToken);
        Task EditRejectByViewModelAsync(CompanyEditModel model);
        Task<Company> FindByIdAsync(Guid companyId);
        Task<Company> FindByAliasAsync(string companyAlias);
        Task<Company> FindByUserIdAsync(Guid userId);
        Task<Location> GetAddressByIdAsync(Guid companyId);
        Task<LocationModel> GetAddressViewModelByIdAsync(Guid companyId);
        Task<object> GetAllNearAsync();
        Task<IList<SelectList>> GetAllAsSelectListItemAsync();
        Task<IList<Company>> GetByRequestAsync(CompanySearchModel model);
        Task<Company> GetByUserIdAsync(Guid userId);
        Task<int> GetCountMyFollowAsync();
        Task<IList<FineUploaderObject>> GetFileAsFineUploaderModelByIdAsync(Guid companyId);
        Task<IList<FineUploaderObject>> GetFileCoverAsFineUploaderModelByIdAsync(Guid companyId);
        Task<bool> IsMySelfAsync(Guid companyId);
        Task<string> GetMyNameByUserIdAsync(Guid userId);
        Task<bool> IsApprovedByAliasAsync(string alias);
        Task<bool> IsExistEmailByCompanyIdAsync(Guid companyId, string email, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> HasAliasByCurrentUserAsync();
        Task<bool> IsExistByAliasAsync(string alias, Guid? companyId = null, bool applyCurrentUser = false);
        Task<bool> IsExistAliasByIdAsync(Guid companyId);
        Task<bool> IsMineByIdAsync(Guid companyId, HttpContext http, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> CompareNameAndSlugAsync(string alias, string slug);
        IQueryable<Company> QueryByRequest(CompanySearchModel model);
        Task SeedAsync();
        Task SetStateByIdAsync(Guid companyId, StateType state);
        Guid CurrentCompanyId { get; }
    }
}