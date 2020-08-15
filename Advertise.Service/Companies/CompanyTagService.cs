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
    public class CompanyTagService : ICompanyTagService
    {
        #region Private Fields

        private readonly IDbSet<CompanyTag> _companyTagRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public CompanyTagService(IMapper mapper, IUnitOfWork unitOfWork, IWebContextManager webContextManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _webContextManager = webContextManager;
            _companyTagRepository = unitOfWork.Set<CompanyTag>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountAllTagByCompanyIdAsync(Guid companyId)
        {
            var request = new CompanyTagSearchModel
            {
                CompanyId = companyId,
            };
            var result = await CountByRequestAsync(request);

            return result;
        }

        public async Task<int> CountByRequestAsync(CompanyTagSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companyTags = await QueryByRequest(model).CountAsync();

            return companyTags;
        }

        public async Task CreateByViewModelAsync(CompanyTagCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companyTag = _mapper.Map<CompanyTag>(model);
            companyTag.CreatedById = _webContextManager.CurrentUserId;
            _companyTagRepository.Add(companyTag);

           await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task  DeleteByIdAsync(Guid companyTagId)
        {
            var companyTag = await _companyTagRepository.FirstOrDefaultAsync(model => model.Id == companyTagId);
            _companyTagRepository.Remove(companyTag);

             await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task EditByViewModelAsync(CompanyTagEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companyTag = await _companyTagRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            _mapper.Map(model, companyTag);

            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task<CompanyTag> FindAsync(Guid companyTagId)
        {
            var companyTag = await _companyTagRepository
                .FirstOrDefaultAsync(model => model.Id == companyTagId);

            return companyTag;
        }

        public IQueryable<CompanyTag> QueryByRequest(CompanyTagSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companyTags = _companyTagRepository.AsNoTracking().AsQueryable();
            if (model.CompanyId.HasValue)
                companyTags = companyTags.Where(m => m.CompanyId == model.CompanyId);
            if (model.CreatedById.HasValue)
                companyTags = companyTags.Where(m => m.CreatedById == model.CreatedById);
            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;
            companyTags = companyTags.OrderBy($"{model.SortMember} {model.SortDirection}");

            return companyTags;
        }

        public async Task<CompanyTagListModel> GetByCompanyIdAsync(Guid companyId)
        {
            var request = new CompanyTagSearchModel
            {
                CompanyId = companyId,
            };
            var result = await ListByRequestAsync(request);

            return result;
        }

        public async Task<IList<CompanyTag>> GetCompanyTagsByRequestAsync(CompanyTagSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companyTags =  await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
            

            return companyTags;
        }

        public async Task<CompanyTagListModel> ListByRequestAsync(CompanyTagSearchModel model)
        {
            model.TotalCount = await CountByRequestAsync(model);
            var companyTags = await GetCompanyTagsByRequestAsync(model);
            var companyTagViewModel = _mapper.Map<IList<CompanyTagModel>>(companyTags);
            var companyTagList = new CompanyTagListModel
            {
                SearchModel = model,
                CompanyTags = companyTagViewModel
            };

            return companyTagList;
        }

        public async Task  SeedAsync()
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods
    }
}