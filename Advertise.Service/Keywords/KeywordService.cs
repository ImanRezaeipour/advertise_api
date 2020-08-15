using Advertise.Data.DbContexts;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Keywords;
using Advertise.Core.Managers.Event;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Keywords;
using Advertise.Service.Keywords;

namespace Advertise.Service.Services.Keywords
{
    public class KeywordService : IKeywordService
    {
        #region Private Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IDbSet<Keyword> _keywordRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public KeywordService(IUnitOfWork unitOfWork, IMapper mapper, IEventPublisher eventPublisher, IWebContextManager webContextManager)
        {
            _unitOfWork = unitOfWork;
            _keywordRepository = unitOfWork.Set<Keyword>();
            _mapper = mapper;
            _eventPublisher = eventPublisher;
            _webContextManager = webContextManager;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task CreateByViewModelAsync(KeywordCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var keyword = _mapper.Map<Keyword>(model);
            keyword.CreatedById = _webContextManager.CurrentUserId;
            _keywordRepository.Add(keyword);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityInserted(keyword);
        }

        public async Task DeleteByIdAsync(Guid keywordId)
        {
            var keyword = await FindByIdAsync(keywordId);
            _keywordRepository.Remove(keyword);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(keyword);
        }

        public async Task<Keyword> FindByIdAsync(Guid keywordId, bool? applyCurrentUser = false)
        {
            var query = _keywordRepository.AsQueryable();
            if (applyCurrentUser == true)
                query = query.Where(model => model.CreatedById == _webContextManager.CurrentUserId);
            return  await query.FirstOrDefaultAsync(model => model.Id == keywordId);
        }

        public async Task<List<Keyword>> GetAllActiveAsync()
        {
            var request = new KeywordSearchModel
            {
                IsActive = true
            };
          return await QueryByRequest(request).ToListAsync();
        }

        public async Task<List<SelectList>> GetAllActiveAsSelectListAsync()
        {
            var request = new KeywordSearchModel
            {
                IsActive = true
            };
           return  await QueryByRequest(request).Select(model => new SelectList
            {
                Text = model.Title,
                Value = model.Id.ToString()
            }).ToListAsync();
        }

        public IQueryable<Keyword> QueryByRequest(KeywordSearchModel model)
        {
            var keywords = _keywordRepository.AsNoTracking().AsQueryable();

            if (model.CreatedOn != null)
                keywords = keywords.Where(m => m.CreatedOn == model.CreatedOn);
            if (model.IsActive != null)
                keywords = keywords.Where(m => m.IsActive == model.IsActive);

            if (model.SortMember == null)
                model.SortMember = SortMember.CreatedOn;
            if (model.SortDirection == null)
                model.SortDirection = SortDirection.Asc;

            keywords = keywords.OrderBy($"{model.SortMember} {model.SortDirection}");

            return keywords;
        }

        public async Task<bool> IsExistByIdAsync(Guid keywordId)
        {
           return await _keywordRepository.AsNoTracking().AnyAsync(model => model.Id == keywordId);
        }

        #endregion Public Methods
    }
}