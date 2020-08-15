using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Companies;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Companies
{
    public class CompanySocialService : ICompanySocialService
    {
        #region Private Fields

        private readonly IDbSet<Company> _companyRepository;
        private readonly IDbSet<CompanySocial> _companySocialRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public CompanySocialService(IMapper mapper, IUnitOfWork unitOfWork, IWebContextManager webContextManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _webContextManager = webContextManager;
            _companyRepository = unitOfWork.Set<Company>();
            _companySocialRepository = unitOfWork.Set<CompanySocial>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(CompanySocialSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companySocials = await QueryByRequest(model).CountAsync();

            return companySocials;
        }

        public async Task CreateByViewModelAsync(CompanySocialCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var social = _mapper.Map<CompanySocial>(model);
            var company = await _companyRepository.FirstOrDefaultAsync(m => m.Id == _webContextManager.CurrentUserId);
            social.CompanyId = company.Id;
            social.CreatedById = _webContextManager.CurrentUserId;
            _companySocialRepository.Add(social);

            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid socialId)
        {
            var social = await _companySocialRepository.FirstOrDefaultAsync(model => model.Id == socialId);
            _companySocialRepository.Remove(social);

            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task EditByViewModelAsync(CompanySocialEditModel model, bool isCurrentUser = false)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (model.Id == Guid.Empty)
            {
                var companySocial = _mapper.Map<CompanySocial>(model);
                companySocial.CreatedById = _webContextManager.CurrentUserId;
                _companySocialRepository.Add(companySocial);

                await _unitOfWork.SaveAllChangesAsync();
            }
            else
            {
                var companySocial = await _companySocialRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
                if(isCurrentUser && companySocial.CreatedById != _webContextManager.CurrentUserId)
                    return;

                _mapper.Map(model, companySocial);

                await _unitOfWork.SaveAllChangesAsync();
            }
        }

        public async Task<CompanySocial> FindAsync(Guid companySocialId)
        {
            var companySocial = await _companySocialRepository
                .FirstOrDefaultAsync(model => model.Id == companySocialId);

            return companySocial;
        }

        public async Task<CompanySocial> FindByUserIdAsync(Guid userId)
        {
            var companySocial = await _companySocialRepository
                .FirstOrDefaultAsync(model => model.CreatedById == userId);

            return companySocial;
        }

        public IQueryable<CompanySocial> QueryByRequest(CompanySocialSearchModel request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var companySocials = _companySocialRepository.AsNoTracking().AsQueryable()
                .Include(model => model.CreatedBy)
                .Include(model => model.CreatedBy.Meta)
                .Include(model => model.Company);
            if (request.CreatedById.HasValue)
                companySocials = companySocials.Where(model => model.CreatedById == request.CreatedById);
            if (request.CompanyId.HasValue)
                companySocials = companySocials.Where(model => model.CompanyId == request.CompanyId);
            if (request.Term.HasValue())
                companySocials = companySocials.Where(model => model.FacebookLink.Contains(request.Term) || model.TelegramLink.Contains(request.Term) || model.InstagramLink.Contains(request.Term) || model.GooglePlusLink.Contains(request.Term) || model.TwitterLink.Contains(request.Term) || model.YoutubeLink.Contains(request.Term));
            if (string.IsNullOrEmpty(request.SortMember))
                request.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(request.SortDirection))
                request.SortDirection = SortDirection.Desc;
            companySocials = companySocials.OrderBy($"{request.SortMember} {request.SortDirection}");

            return companySocials;
        }

        public async Task<IList<CompanySocial>> GetByRequestAsync(CompanySocialSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companySocials = await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);

            return companySocials;
        }

        public async Task RemoveRangeAsync(IList<CompanySocial> companySocials)
        {
            if (companySocials == null)
                throw new ArgumentNullException(nameof(companySocials));

            foreach (var companySocial in companySocials)
                _companySocialRepository.Remove(companySocial);
        }

        public async Task SeedAsync()
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods
    }
}