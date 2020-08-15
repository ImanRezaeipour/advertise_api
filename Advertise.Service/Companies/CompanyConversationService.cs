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
using Advertise.Data.DbContexts;
using Advertise.Service.Users;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Advertise.Service.Companies
{
    public class CompanyConversationService : ICompanyConversationService
    {
        #region Private Fields

        private readonly IDbSet<CompanyConversation> _conversationRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;
        private readonly IUserService _userService;

        #endregion Private Fields

        #region Public Constructors

        public CompanyConversationService(IMapper mapper, IUnitOfWork unitOfWork, IWebContextManager webContextManager, IEventPublisher eventPublisher, IUserService userService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _webContextManager = webContextManager;
            _eventPublisher = eventPublisher;
            _userService = userService;
            _conversationRepository = unitOfWork.Set<CompanyConversation>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(CompanyConversationSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            return await QueryByRequest(model).CountAsync();
        }

        public async Task CreateByViewModelAsync(CompanyConversationCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var conversation = _mapper.Map<CompanyConversation>(model);
            conversation.IsRead = false;
            conversation.CreatedById = _webContextManager.CurrentUserId;
            conversation.CreatedOn = DateTime.Now;

            _conversationRepository.Add(conversation);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityInserted(conversation);
        }

        public async Task DeleteByIdAsync(Guid conversationId)
        {
            var conversation = await _conversationRepository.FirstOrDefaultAsync(model => model.Id == conversationId);
            _conversationRepository.Remove(conversation);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(conversation);
        }

        public async Task EditByViewModelAsync(CompanyConversationEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var conversation = await _conversationRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            _mapper.Map(model, conversation);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(conversation);
        }

        public async Task<CompanyConversation> FindByIdAsync(Guid conversationId)
        {
            return await _conversationRepository
                   .FirstOrDefaultAsync(model => model.Id == conversationId);
        }

        public async Task<IList<CompanyConversation>> GetByRequestAsync(CompanyConversationSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public async Task<List<CompanyConversationModel>> GetListByUserIdAsync(Guid userId)
        {
            var list = await _conversationRepository
                .AsNoTracking()
                .Where(model => model.CreatedById == _webContextManager.CurrentUserId && model.ReceivedById == userId ||
                                model.CreatedById == userId && model.ReceivedById == _webContextManager.CurrentUserId)
                .OrderBy(model => model.CreatedOn)
                .ProjectTo<CompanyConversationModel>()
                .ToListAsync();
            if (list.Count > 0)
            {
                var currentUser = (await _userService.FindByIdAsync(_webContextManager.CurrentUserId)).CreatedBy;

                var createdBy =
                 (await _conversationRepository.FirstOrDefaultAsync(model => model.CreatedById == userId)).CreatedBy;
                foreach (var iteModel in list)
                {
                    if (iteModel.CreatedBy.Id == _webContextManager.CurrentUserId)
                        iteModel.AvatarFileName = currentUser.Meta.AvatarFileName;
                    else
                        iteModel.AvatarFileName = createdBy.Meta.AvatarFileName;
                }
            }

            return list;
        }

        public async Task<List<SelectList>> GetUsersAsSelectListAsync()
        {
            return await _conversationRepository
                .Include(model => model.CreatedBy)
                .Include(model => model.CreatedBy.Meta)
                .AsNoTracking()
                .Where(model => model.ReceivedById == _webContextManager.CurrentUserId)
                .Select(model => new SelectList
                {
                    Text = model.CreatedBy.Meta.DisplayName ?? (model.CreatedBy.Meta.LastName ?? (model.CreatedBy.UserName ?? model.CreatedBy.Email)),
                    Value = model.CreatedById.ToString()
                })
                .Distinct()
                .ToListAsync();
        }

        public IQueryable<CompanyConversation> QueryByRequest(CompanyConversationSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var conversations = _conversationRepository.AsNoTracking().AsQueryable();
            if (model.CreatedById.HasValue)
                conversations = conversations.Where(m => m.CreatedById == model.CreatedById);
            if (model.RecivedById.HasValue)
                conversations = conversations.Where(m => m.ReceivedById == model.RecivedById);
            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;
            conversations = conversations.OrderBy($"{model.SortMember} {model.SortDirection}");

            return conversations;
        }

        public async Task SeedAsync()
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods
    }
}