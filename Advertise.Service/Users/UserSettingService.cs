using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Users;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Users;
using Advertise.Core.Types;
using Advertise.Data.DbContexts;
using Advertise.Service.Common;
using AutoMapper;

namespace Advertise.Service.Users
{
    public class UserSettingService : IUserSettingService
    {
        #region Private Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<UserSetting> _userSettingRepository;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public UserSettingService(IMapper mapper, IUnitOfWork unitOfWork, IWebContextManager webContextManager, IEventPublisher eventPublisher)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _webContextManager = webContextManager;
            _eventPublisher = eventPublisher;
            _userSettingRepository = unitOfWork.Set<UserSetting>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(UserSettingSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).CountAsync();
        }

        public async Task CreateByUserIdAsync(Guid userId)
        {
            var userSetting = new UserSetting
            {
                CreatedById = userId,
                Language = LanguageType.Persian,
                Theme = ThemeType.White
            };
            _userSettingRepository.Add(userSetting);

            await _unitOfWork.SaveAllChangesAsync(auditUserId: _webContextManager.CurrentUserId);
            _eventPublisher.EntityInserted(userSetting);
        }

        public async Task CreateByViewModelAsync(UserSettingCreateModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentException(nameof(viewModel));

            var userSetting = _mapper.Map<UserSetting>(viewModel);
            userSetting.CreatedById = _webContextManager.CurrentUserId;
            _userSettingRepository.Add(userSetting);

            await _unitOfWork.SaveAllChangesAsync(auditUserId: _webContextManager.CurrentUserId);
            _eventPublisher.EntityInserted(userSetting);
        }

        public async Task DeleteByIdAsync(Guid userSettingId)
        {
            var userSetting = await _userSettingRepository.FirstOrDefaultAsync(model => model.Id == userSettingId);
            _userSettingRepository.Remove(userSetting);

            await _unitOfWork.SaveAllChangesAsync(auditUserId: _webContextManager.CurrentUserId);
            _eventPublisher.EntityDeleted(userSetting);
        }

        public async Task EditByViewModelAsync(UserSettingEditModel model, bool applyCurrentUser = false)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var userSetting = await _userSettingRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            if(applyCurrentUser && userSetting.CreatedById == _webContextManager.CurrentUserId)
                return;

            _mapper.Map(model, userSetting);

            await _unitOfWork.SaveAllChangesAsync(auditUserId: _webContextManager.CurrentUserId);
            _eventPublisher.EntityUpdated(userSetting);
        }

        public async Task<UserSetting> FindByIdAsync(Guid userSettingId)
        {
            return await _userSettingRepository
                 .FirstOrDefaultAsync(model => model.Id == userSettingId);
        }

        public async Task<UserSetting> FindByUserIdAsync(Guid userId)
        {
            return await _userSettingRepository
                .FirstOrDefaultAsync(model => model.CreatedById == userId);
        }

        public async Task<IList<UserSetting>> GetByRequestAsync(UserSettingSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public async Task<string> GetMyLanguageAsync()
        {
            var setting = await FindByUserIdAsync(_webContextManager.CurrentUserId);

            if (setting == null)
                return string.Empty;

            switch (setting.Language)
            {
                case LanguageType.English:
                    return "en-US";

                case LanguageType.Persian:
                    return "fa-IR";

                default:
                    return "en-US";
            }
        }

        public IQueryable<UserSetting> QueryByRequest(UserSettingSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var userSettings = _userSettingRepository.AsNoTracking().AsQueryable();
            if (model.CreatedById.HasValue)
                userSettings = userSettings.Where(userSetting => userSetting.CreatedById == model.CreatedById);
            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;
            userSettings = userSettings.OrderBy($"{model.SortMember} {model.SortDirection}");

            return userSettings;
        }

        public async Task<ServiceResult> SeedAsync()
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods
    }
}