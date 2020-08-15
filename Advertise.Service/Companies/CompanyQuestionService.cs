using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Companies;
using Advertise.Core.Types;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Companies
{
    public class CompanyQuestionService : ICompanyQuestionService
    {
        #region Private Fields

        private readonly IDbSet<CompanyQuestion> _companyQuestionRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public CompanyQuestionService(IMapper mapper, IUnitOfWork unitOfWork, IWebContextManager webContextManager, IEventPublisher eventPublisher)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _webContextManager = webContextManager;
            _eventPublisher = eventPublisher;
            _companyQuestionRepository = unitOfWork.Set<CompanyQuestion>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task ApproveByViewModelAsync(CompanyQuestionEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companyQuestion = await FindByIdAsync(model.Id);
            _mapper.Map(model, companyQuestion);
            companyQuestion.ApprovedById = _webContextManager.CurrentUserId;
            companyQuestion.State = StateType.Approved;

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(companyQuestion);
        }

        public async Task<int> CountByRequestAsync(CompanyQuestionSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).CountAsync();
        }

        public async Task CreateByViewModelAsync(CompanyQuestionCreateModel model)
        {
            if (model == null)
                throw new ArgumentException(nameof(model));

            var companyQuestion = _mapper.Map<CompanyQuestion>(model);
            companyQuestion.State = StateType.Pending;
            companyQuestion.CreatedById = _webContextManager.CurrentUserId;
            companyQuestion.CreatedOn = DateTime.Now;
           _companyQuestionRepository.Add(companyQuestion);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityInserted(companyQuestion);
        }

        public async Task DeleteByIdAsync(Guid companyQuestionId)
        {
            var companyQuestion = await FindByIdAsync(companyQuestionId);
            _companyQuestionRepository.Remove(companyQuestion);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(companyQuestion);
        }

       public async Task EditByViewModelAsync(CompanyQuestionEditModel model)
        {
            if (model == null)
                throw new ArgumentException(nameof(model));

            var companyQuestion = await FindByIdAsync(model.Id);
            _mapper.Map(model, companyQuestion);
            companyQuestion.ModifiedById = _webContextManager.CurrentUserId;

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(companyQuestion);
        }

        public async Task<CompanyQuestion> FindByIdAsync(Guid companyQuestionId)
        {
            return await _companyQuestionRepository
                .SingleOrDefaultAsync(question => question.Id == companyQuestionId);
        }

        public async Task<IList<CompanyQuestion>> GetByCompanyIdAsync(Guid companyId)
        {
            return await _companyQuestionRepository.AsNoTracking()
                .Where(question => question.CompanyId == companyId)
                .ToListAsync();
        }

        public async Task<IList<CompanyQuestion>> GetByRequestAsync(CompanyQuestionSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public async Task<IList<CompanyQuestion>> GetAllByCompanyIdAsync(Guid companyId)
        {
            var companies = await _companyQuestionRepository.AsNoTracking()
                .Where(model => model.State == StateType.Approved && model.CompanyId == companyId)
                .OrderByDescending(model => model.CreatedOn)
                .ToListAsync();

            var tempList = new List<CompanyQuestion>();
            foreach (var company in companies.Where(model => model.ReplyId == null))
            {
                tempList.Add(company);
                tempList.AddRange(companies.Where(model => model.ReplyId == company.Id));
                
            }
            return tempList;
        }

        public IQueryable<CompanyQuestion> QueryByRequest(CompanyQuestionSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companyQuestions = _companyQuestionRepository.AsNoTracking().AsQueryable();

            if (model.CreatedById.HasValue)
                companyQuestions = companyQuestions.Where(m => m.CreatedById == model.CreatedById);
            if (model.State.HasValue)
                companyQuestions = companyQuestions.Where(m => m.State == model.State);
            if (model.CompanyId.HasValue)
                companyQuestions = companyQuestions.Where(m => m.CompanyId == model.CompanyId);

            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;

            companyQuestions = companyQuestions.OrderBy($"{model.SortMember} {model.SortDirection}");

            return companyQuestions;
        }

        public async Task RejectByViewModelAsync(CompanyQuestionEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companyQuestion = await FindByIdAsync(model.Id);
            _mapper.Map(model, companyQuestion);
            companyQuestion.State = StateType.Rejected;

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(companyQuestion);
        }

        public async Task RemoveRangeByCompanyId(Guid companyId)
        {
            var companyQuestions = await GetByCompanyIdAsync(companyId);
            foreach (var companyQuestion in companyQuestions)
                await DeleteByIdAsync(companyQuestion.Id);
        }

        #endregion Public Methods
    }
}