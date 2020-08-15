using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Advertise.Core.Common;
using Advertise.Core.Domain.Categories;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Domain.Locations;
using Advertise.Core.Domain.Users;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Managers.ExportImport;
using Advertise.Core.Managers.ExportImport.Help;
using Advertise.Core.Managers.File;
using Advertise.Core.Managers.Html;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Companies;
using Advertise.Core.Model.Locations;
using Advertise.Core.Types;
using Advertise.Data.DbContexts;
using Advertise.Service.Locations;
using Advertise.Service.Services.Permissions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
using FineUploaderObject = Advertise.Core.Objects.FineUploaderObject;

namespace Advertise.Service.Companies
{
    public class CompanyService : ICompanyService
    {
        #region Private Fields

        private readonly ILocationService _addressService;
        private readonly ILocationCityService _cityService;
        private readonly IDbSet<Category> _categoryRepository;
        private readonly ICompanyAttachmentService _companyAttachmentService;
        private readonly IDbSet<CompanyFollow> _companyFollowRepository;
        private readonly ICompanyFollowService _companyFollowService;
        private readonly ICompanyImageService _companyImageService;
        private readonly ICompanyQuestionService _companyQuestionService;
        private readonly IDbSet<Company> _companyRepository;
        private readonly ICompanyReviewService _companyReviewService;
        private readonly ICompanySocialService _companySocialService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<User> _userRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public CompanyService(IMapper mapper, ICompanyReviewService companyReviewService,
            ICompanyFollowService companyFollowService, ICompanyImageService companyImageService,
            ICompanyAttachmentService companyAttachmentService, ICompanyQuestionService companyQuestionService,
            ICompanySocialService companySocialService, IUnitOfWork unitOfWork,
            IWebContextManager webContextManager, ILocationService addressService, IEventPublisher eventPublisher, ILocationCityService cityService)
        {
            _mapper = mapper;
            _companyReviewService = companyReviewService;
            _companyFollowService = companyFollowService;
            _companyImageService = companyImageService;
            _companyAttachmentService = companyAttachmentService;
            _companyQuestionService = companyQuestionService;
            _companySocialService = companySocialService;
            _companyRepository = unitOfWork.Set<Company>();
            _unitOfWork = unitOfWork;
            _webContextManager = webContextManager;
            _addressService = addressService;
            _eventPublisher = eventPublisher;
            _cityService = cityService;
            _categoryRepository = unitOfWork.Set<Category>();
            _companyFollowRepository = unitOfWork.Set<CompanyFollow>();
            _userRepository = unitOfWork.Set<User>();
        }

        #endregion Public Constructors
        
        public Guid CurrentCompanyId => _companyRepository.AsNoTracking().Where(m => m.CreatedById == _webContextManager.CurrentUserId).Select(m => m.Id).SingleOrDefault();

        #region Public Methods

        public async Task<int> CountAllAsync()
        {
            var request = new CompanySearchModel
            {
                PageSize = PageSize.All
            };
            return await CountByRequestAsync(request);
        }

        public async Task<bool> HasAliasAsync(Guid input, CancellationToken cancellationToken)
        {
            return await HasAliasByCurrentUserAsync();
        }

        public async Task<int> CountByRequestAsync(CompanySearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).CountAsync();
        }

        public async Task<int> CountByStateAsync(StateType companyState)
        {
            var request = new CompanySearchModel
            {
                State = companyState
            };
            return await CountByRequestAsync(request);
        }

        public async Task<int> CountByCategoryIdAsync(Guid categoryId)
        {
            var request = new CompanySearchModel
            {
                CategoryId = categoryId
            };
            return await CountByRequestAsync(request);
        }

        public async Task CreateByUserIdAsync(Guid userId)
        {
            var defaultLocation = await _addressService.GetDefaultLocationAsync();

            var company = new Company
            {
                CreatedById = userId,
                State = StateType.Approved,
                CategoryId = (await _categoryRepository.FirstAsync(model => model.ParentId == null)).Id,
                CreatedOn = DateTime.Now,
                Location = new Location()
                {
                    Latitude = defaultLocation.Item1,
                    Longitude = defaultLocation.Item2,
                    CityId = await _cityService.GetIdByNameAsync(defaultLocation.Item3),
                    CreatedById = _webContextManager.CurrentUserId
                }
            };
            _companyRepository.Add(company);
        }

        public async Task CreateEasyByViewModelAsync(CompanyCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var company = _mapper.Map<Company>(model);
            company.State = StateType.Pending;
            company.CreatedOn = DateTime.Now;

            var defaultLocation = await _addressService.GetDefaultLocationAsync();
            company.Location = new Location()
            {
                Latitude = defaultLocation.Item1,
                Longitude = defaultLocation.Item2,
                CityId = await _cityService.GetIdByNameAsync(defaultLocation.Item3),
                CreatedById = _webContextManager.CurrentUserId
            };

            _companyRepository.Add(company);

            await _unitOfWork.SaveAllChangesAsync(auditUserId: model.CreatedById);

            _eventPublisher.EntityInserted(company);
        }

        public async Task DeleteByAliasAsync(string companyAlias, bool isCurrentUser = false)
        {
            var company = await FindByAliasAsync(companyAlias);
            if(isCurrentUser && company.CreatedById != _webContextManager.CurrentUserId)
                return;

            await _companyAttachmentService.RemoveRangeAsync(company.Attachments.ToList());
            await _companyFollowService.RemoveRangeAsync(company.Follows.ToList());
            await _companyImageService.RemoveRangeAsync(company.Images.ToList());
            await _companyReviewService.RemoveRangeAsync(company.Reviews.ToList());
            await _companySocialService.RemoveRangeAsync(company.Socials.ToList());
            await _companyQuestionService.RemoveRangeByCompanyId(company.Id);
            _companyRepository.Attach(company);
            _companyRepository.Remove(company);
            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(company);
        }

        public async Task DeleteByUserIdAsync(Guid userId)
        {
            var company = await GetByUserIdAsync(userId);
            await DeleteByAliasAsync(company.Alias);
        }

        public async Task EditApproveByViewModelAsync(CompanyEditModel model)
        {
            var orginalCompany = await _companyRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            await EditByViewModelAsync(model);
            orginalCompany.Alias = orginalCompany.Alias;
            orginalCompany.State = StateType.Approved;
            orginalCompany.Description = model.Description.ToSafeHtml();

            await _unitOfWork.SaveAllChangesAsync();
            _eventPublisher.EntityUpdated(orginalCompany);
        }

        public async Task EditByViewModelAsync(CompanyEditModel model, bool isCurrentUser = false)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var addressOriginal = await _addressService.FindByIdAsync(model.AddressId);
            if(isCurrentUser && addressOriginal.CreatedById != _webContextManager.CurrentUserId)
                return;

            addressOriginal.CityId = model.Location.LocationCity.Id;
            addressOriginal.Latitude = model.Location.Latitude;
            addressOriginal.Longitude = model.Location.Longitude;
            addressOriginal.Extra = model.Location.Extra;
            addressOriginal.PostalCode = model.Location.PostalCode;
            addressOriginal.Street = model.Location.Street;

            var originalCompany = await FindByIdAsync(model.Id);
            if (originalCompany.Alias != null)
                model.Alias = originalCompany.Alias;

            _mapper.Map(model, originalCompany);
            originalCompany.Description = model.Description.ToSafeHtml();

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(originalCompany);
        }

        public async Task<bool> IsExistAliasCancellationTokenAsync(string alias,HttpContext http, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(http.User.Identity.GetUserId());
            var originUser = await _companyRepository.AsNoTracking().SingleOrDefaultAsync(company => company.CreatedById == userId, cancellationToken);
            if (originUser != null && alias == originUser.Alias || http.User.IsInRole("CanCompanyEdit") )
                return false;

            return await _companyRepository.AsNoTracking().AnyAsync(company => company.Alias == alias, cancellationToken);
        }

        public async Task EditRejectByViewModelAsync(CompanyEditModel model)
        {
            var orginalCompany = await _companyRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            await EditByViewModelAsync(model);
            orginalCompany.State = StateType.Rejected;
            orginalCompany.Alias = orginalCompany.Alias;

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(orginalCompany);
        }

        public async Task<Company> FindByIdAsync(Guid companyId)
        {
            return await _companyRepository
                .FirstOrDefaultAsync(model => model.Id == companyId);
        }

        public async Task<Company> FindByAliasAsync(string companyAlias)
        {
            return await _companyRepository.AsNoTracking()
                .Include(model => model.Location)
                .FirstOrDefaultAsync(model => model.Alias == companyAlias);
        }

        public async Task<Company> FindByUserIdAsync(Guid userId)
        {
            var company = await _companyRepository
                .FirstOrDefaultAsync(model => model.CreatedById == userId);
            return company;
        }

        public async Task<Location> GetAddressByIdAsync(Guid companyId)
        {
            var company = await FindByIdAsync(companyId);
            return company.Location;
        }

        public async Task<LocationModel> GetAddressViewModelByIdAsync(Guid companyId)
        {
            var company = await FindByIdAsync(companyId);

            var address = await _addressService.FindByIdAsync(company.LocationId.GetValueOrDefault()) ?? new Location();
            return _mapper.Map<LocationModel>(address);
        }

        public async Task<object> GetAllNearAsync()
        {
            var request = new CompanySearchModel
            {
                PageSize = PageSize.All,
                State = StateType.Approved
            };
            var companies = await GetByRequestAsync(request);
            var viewModel = _mapper.Map<List<CompanyModel>>(companies);
            return viewModel.Select(model => new
            {
                model.CategoryAlias,
                model.Title,
                model.Id,
                model.CategoryCode,
                model.Alias,
                model.Location.Latitude,
                model.Location.Longitude,
                model.CategoryMetaTitle,
                model.CategoryTitle
            });
        }

        public async Task<IList<SelectList>> GetAllAsSelectListItemAsync()
        {
            var request = new CompanySearchModel
            {
                PageSize = PageSize.All
            };
            return await QueryByRequest(request).ProjectTo<SelectList>().ToListAsync();
        }

        public async Task<IList<Company>> GetByRequestAsync(CompanySearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public async Task<Company> GetByUserIdAsync(Guid userId)
        {
            return await _companyRepository.FirstOrDefaultAsync(model => model.CreatedById == userId);
        }

        public async Task<int> GetCountMyFollowAsync()
        {
            var currentCompanyId = CurrentCompanyId;
            return await _companyFollowRepository
            .Where(model => model.CompanyId == currentCompanyId && model.IsFollow == true)
            .Select(model => model.FollowedById)
            .Distinct().CountAsync();
        }

        public async Task<IList<FineUploaderObject>> GetFileAsFineUploaderModelByIdAsync(Guid companyId)
        {
            return (await _companyRepository.AsNoTracking()
                    .Where(model => model.Id == companyId)
                    .Select(model => new
                    {
                        model.Id,
                        model.LogoFileName
                    }).ToListAsync())
                .Select(model => new FineUploaderObject
                {
                    id = model.Id.ToString(),
                    uuid = model.Id.ToString(),
                    name = model.LogoFileName,
                    thumbnailUrl = FileConst.ImagesWebPath.PlusWord(model.LogoFileName) ?? FileConst.NoLogo,
                    size = new FileInfo(HttpContext.Current.Server.MapPath(FileConst.ImagesWebPath.PlusWord(model.LogoFileName))).Length.ToString()
                }).ToList();
        }

        public async Task<IList<FineUploaderObject>> GetFileCoverAsFineUploaderModelByIdAsync(Guid companyId)
        {
            return (await _companyRepository.AsNoTracking()
                    .Where(model => model.Id == companyId)
                    .Select(model => new
                    {
                        model.Id,
                        model.CoverFileName
                    }).ToListAsync())
                .Select(model => new FineUploaderObject
                {
                    id = model.Id.ToString(),
                    uuid = model.Id.ToString(),
                    name = model.CoverFileName,
                    thumbnailUrl = FileConst.ImagesWebPath.PlusWord(model.CoverFileName) ?? FileConst.NoLogo,
                    size = new FileInfo(HttpContext.Current.Server.MapPath(FileConst.ImagesWebPath.PlusWord(model.CoverFileName))).Length.ToString()
                }).ToList();
        }

        public async Task<bool> IsMySelfAsync(Guid companyId)
        {
            return await _companyRepository.AsNoTracking()
                .AnyAsync(model => model.CreatedById == _webContextManager.CurrentUserId && model.Id == companyId);
        }

        public async Task<string> GetMyNameByUserIdAsync(Guid userId)
        {
            var user = await _userRepository.FirstOrDefaultAsync(model => model.Id == userId);
            if (user == null)
            {
                return "";
            }

            if (user.Meta.DisplayName != null)
                return user.Meta.DisplayName;
            if (user.Meta.FullName != " ")
                return user.Meta.FullName;
            if (user.CreatedBy.UserName != null)
                return user.CreatedBy.UserName;
            return user.CreatedBy.Email;
        }

        public async Task<bool> IsApprovedByAliasAsync(string alias)
        {
            return await _companyRepository.AnyAsync(model => model.Alias == alias && model.State == StateType.Approved);
        }

        public async Task<bool> IsExistEmailByCompanyIdAsync(Guid companyId, string email, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (email == null)
                return false;

            return await _companyRepository.AnyAsync(model => model.Email == email && model.Id != companyId && email != null);
        }

        public async Task<bool> HasAliasByCurrentUserAsync()
        {
            return await _companyRepository.AsNoTracking()
                .AnyAsync(model => model.CreatedById == _webContextManager.CurrentUserId && model.Alias != null);
        }

        public async Task<bool> IsExistByAliasAsync(string alias, Guid? companyId = null, bool applyCurrentUser = false)
        {
            var query = _companyRepository.AsNoTracking().AsQueryable();
            query = query.Where(model => model.Alias == alias);

            if (applyCurrentUser)
                query = query.Where(model => model.CreatedById == _webContextManager.CurrentUserId);

            if (companyId.HasValue)
            {
                query = query.Where(model => model.Id != companyId);
                query = query.Where(model => model.Id == companyId);
            }

            return await query.AnyAsync();
        }

        public async Task<bool> IsExistAliasByIdAsync(Guid companyId)
        {
            return await _companyRepository.AsNoTracking().AnyAsync(model => model.Id == companyId && model.Alias != null);
        }

        public async Task<bool> IsMineByIdAsync(Guid companyId, HttpContext http, CancellationToken cancellationToken = default(CancellationToken))
        {
            var company = await _companyRepository.FirstOrDefaultAsync(model => model.Id == companyId);
            return (company.CreatedById == http.User.Identity.GetUserId().ToGuid() || http.User.IsInRole(PermissionConst.CanCompanyEdit));
        }

        public async Task<bool> CompareNameAndSlugAsync(string alias, string slug)
        {
            if (slug == "")
                return true;

            var company = await _companyRepository.AsNoTracking().FirstOrDefaultAsync(model => model.Alias == alias);
            if (company.Title.CastToSlug() == slug)
                return true;

            return false;
        }

        public IQueryable<Company> QueryByRequest(CompanySearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companies = _companyRepository.AsNoTracking().AsQueryable();

            if (model.Term.HasValue())
                companies = companies.Where(company => company.Title.Contains(model.Term) || company.Description.Contains(model.Term));
            if (model.State.HasValue)
                companies = companies.Where(company => company.State == model.State);
            if (model.CategoryId.HasValue)
                companies = companies.Where(company => company.CategoryId == model.CategoryId);
            if (model.CompanyId.HasValue)
                companies = companies.Where(company => company.Id == model.CompanyId);
            if (model.CreatedById.HasValue)
                companies = companies.Where(m => m.CreatedById == model.CreatedById);

            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;

            companies = companies.OrderBy($"{model.SortMember} {model.SortDirection}");

            return companies;
        }

        public async Task SeedAsync()
        {
            throw new NotImplementedException();
        }

        public async Task SetStateByIdAsync(Guid companyId, StateType state)
        {
            var company = await FindByIdAsync(companyId);
            company.State = state;

            await _unitOfWork.SaveAllChangesAsync();
        }

        private async Task<byte[]> ExportCompanyCatalogsToXlsxAsync(IEnumerable<CompanyCatalogModel> companyCatalogs)
        {
            var properties = new[]
            {
                new PropertyByName<CompanyCatalogModel>("price", p => p.Price)
            };

            return new ExportManager().ExportToXlsx(properties, companyCatalogs);
        }

        #endregion Public Methods
    }
}