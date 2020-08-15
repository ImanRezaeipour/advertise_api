using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Notices;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Managers.Html;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Notices;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Notices
{
    public class NewsService : INoticeService
    {
        #region Private Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IDbSet<Notice> _newsRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public NewsService(IMapper mapper, IUnitOfWork unitOfWork, IEventPublisher eventPublisher)
        {
            _mapper = mapper;
            _newsRepository = unitOfWork.Set<Notice>();
            _unitOfWork = unitOfWork;
            _eventPublisher = eventPublisher;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(NoticeSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).CountAsync();
        }

        public async Task CreateByViewModelAsync(NoticeCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var news = _mapper.Map<Notice>(model);
            news.Body = news.Body.ToSafeHtml();
            news.CreatedOn = DateTime.Now;
            _newsRepository.Add(news);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityInserted(news);
        }

        public async Task DeleteByIdAsync(Guid newsId)
        {
            var news = await _newsRepository.FirstOrDefaultAsync(model => model.Id == newsId);
            _newsRepository.Remove(news);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(news);
        }

        public async Task EditByViewModelAsync(NoticeEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var news = await _newsRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            _mapper.Map(model, news);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(news);
        }

        public async Task<Notice> FindByIdAsync(Guid newsId)
        {
            return await _newsRepository
                 .FirstOrDefaultAsync(model => model.Id == newsId);
        }

        public async Task<IList<Notice>> GetByRequestAsync(NoticeSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public IQueryable<Notice> QueryByRequest(NoticeSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var newses = _newsRepository.AsNoTracking().AsQueryable();
            if (model.Term.HasValue())
                newses = newses.Where(m => m.Title.Contains(model.Term));
            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;
            newses = newses.OrderBy($"{model.SortMember} {model.SortDirection}");

            return newses;
        }

        public async Task SeedAsync()
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods
    }
}