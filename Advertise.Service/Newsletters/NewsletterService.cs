using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Newsletters;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Newsletters;
using Advertise.Core.Types;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Newsletters
{
    public class NewsletterService : INewsletterService
    {
        #region Private Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IDbSet<Newsletter> _newsletterRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public NewsletterService(IMapper mapper, IUnitOfWork unitOfWork, IEventPublisher eventPublisher, IWebContextManager webContextManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _eventPublisher = eventPublisher;
            _webContextManager = webContextManager;
            _newsletterRepository = unitOfWork.Set<Newsletter>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(NewsletterSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException();

            return await QueryByRequest(model).CountAsync();
        }

        public async Task CreateByViewModelAsync(NewsletterCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var newsletter = _mapper.Map<Newsletter>(model);
            newsletter.CreatedById = _webContextManager.CurrentUserId;
            _newsletterRepository.Add(newsletter);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityInserted(newsletter);
        }

        public async Task DeleteByIdAsync(Guid newsletterId)
        {
            if (newsletterId == Guid.Empty)
                throw new ArgumentNullException(nameof(newsletterId));

            var newsletter = await _newsletterRepository.FirstOrDefaultAsync(model => model.Id == newsletterId);
            _newsletterRepository.Remove(newsletter);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(newsletter);
        }

        public async Task<IList<Newsletter>> GetByRequestAsync(NewsletterSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException();

            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public async Task<bool> IsEmailExistAsync(string email, Guid? userId = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = _newsletterRepository.AsQueryable();
            query = query.Where(model => model.Email.ToLower() == email.ToLower());
            if (userId.HasValue)
                query = query.Where(model => model.CreatedById == userId);

            return await query.AnyAsync();
        }

        public IQueryable<Newsletter> QueryByRequest(NewsletterSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException();

            var newsletter = _newsletterRepository.AsNoTracking().AsQueryable();
            if (model.CreatedById.HasValue)
                newsletter = newsletter.Where(m => m.CreatedById == model.CreatedById);
            if (model.Meet.HasValue)
                newsletter = newsletter.Where(m => m.Meet == model.Meet);
            if (model.Email.HasValue)
                newsletter = newsletter.Where(m => m.Email.Contains(model.Term));
            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;
            newsletter = newsletter.OrderBy($"{model.SortMember} {model.SortDirection}");

            return newsletter;
        }

        public async Task SubscribeByViewModelAsync(NewsletterCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException();

            var newsletter = _mapper.Map<Newsletter>(model);
            newsletter.Meet = MeetType.Guest;
            _newsletterRepository.Add(newsletter);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityInserted(newsletter);
        }

        #endregion Public Methods
    }
}