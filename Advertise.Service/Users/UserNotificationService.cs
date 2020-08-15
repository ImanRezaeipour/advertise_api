using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Categories;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Domain.Products;
using Advertise.Core.Domain.Users;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Users;
using Advertise.Core.Types;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Users
{
    public class UserNotificationService : IUserNotificationService
    {
        #region Private Fields

        private readonly IDbSet<Category> _categoryRepository;
        private readonly IDbSet<Company> _companyRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IDbSet<UserNotification> _notificationRepository;
        private readonly IDbSet<Product> _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public UserNotificationService(IMapper mapper, IUnitOfWork unitOfWork, IWebContextManager webContextManager, IEventPublisher eventPublisher)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _webContextManager = webContextManager;
            _eventPublisher = eventPublisher;
            _categoryRepository = unitOfWork.Set<Category>();
            _companyRepository = unitOfWork.Set<Company>();
            _productRepository = unitOfWork.Set<Product>();
            _notificationRepository = unitOfWork.Set<UserNotification>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(UserNotificationSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).CountAsync();
        }

        public async Task CreateAsync(Guid productId)
        {
            var notification = new UserNotification();

            var product = await _productRepository.FirstOrDefaultAsync(model => model.Id == productId);
            var companyList = await _companyRepository.Include(model => model.Follows).FirstOrDefaultAsync(model => model.Id == product.CompanyId);
            var userCompanyList = companyList.Follows.Select(model => model.FollowedBy);
            var categoryList = await _categoryRepository.Include(model => model.Follows).FirstOrDefaultAsync(model => model.Id == product.CategoryId);
            var userCategoryList = categoryList.Follows.Select(model => model.FollowedBy);

            var userList = userCompanyList.Concat(userCategoryList);
            foreach (var user in userList)
            {
                notification.CreatedById = user.Id;
                notification.Message = "محصول " + product.Title.PlusWord(" ") + " اضافه شد ";
                notification.TargetId = productId;
                notification.Type = NotificationType.NewProduct;
                notification.TargetTitle = product.Title;
                notification.OwnedById = user.Id;
                notification.IsRead = false;
                notification.CreatedOn = DateTime.Now;
                _notificationRepository.Add(notification);
            }
            await _unitOfWork.SaveAllChangesAsync();
            _eventPublisher.EntityInserted(notification);
        }

        public async Task CreateByViewModelAsync()
        {
            var notification = new UserNotification();

            var product = await _productRepository.FirstOrDefaultAsync(model => model.Id == notification.TargetId.Value);
            var companyList = await _companyRepository.Include(model => model.Follows).FirstOrDefaultAsync(model => model.Id == product.CompanyId);
            var userCompanyList = companyList.Follows.Select(model => model.FollowedBy);
            var categoryList = await _categoryRepository.Include(model => model.Follows).FirstOrDefaultAsync(model => model.Id == product.CategoryId);
            var userCategoryList = categoryList.Follows.Select(model => model.FollowedBy);

            var userList = userCompanyList.Concat(userCategoryList);
            foreach (var user in userList)
            {
                notification.CreatedById = user.Id;
                notification.Message = "محصول  " + notification.TargetTitle.PlusWord(" ") + " اضافه شد ";
                _notificationRepository.Add(notification);
                await _unitOfWork.SaveAllChangesAsync();
            }

            _eventPublisher.EntityInserted(notification);
        }

        public async Task DeleteAllReadByCurrentUserAsync()
        {
            var notifications = await _notificationRepository.Where(model => model.IsRead == true && model.CreatedById == _webContextManager.CurrentUserId).ToListAsync();
            foreach (var notification in notifications)
            {
                _notificationRepository.Remove(notification);
            }

            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid notificationId)
        {
            var notification = await _notificationRepository.FirstOrDefaultAsync(model => model.Id == notificationId);
            _notificationRepository.Remove(notification);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(notification);
        }

        public async Task<UserNotification> FindByIdAsync(Guid notificationId)
        {
            return await _notificationRepository
                 .FirstOrDefaultAsync(model => model.Id == notificationId);
        }

        public async Task<IList<UserNotification>> GetByRequestAsync(UserNotificationSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public IQueryable<UserNotification> QueryByRequest(UserNotificationSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var notifications = _notificationRepository.AsNoTracking().AsQueryable();
            if (model.CreatedById.HasValue)
                notifications = notifications.Where(m => m.CreatedById == model.CreatedById);
            if (model.CreatedById.HasValue)
                notifications = notifications.Where(m => m.CreatedById == model.CreatedById);
            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;
            notifications = notifications.OrderBy($"{model.SortMember} {model.SortDirection}");

            return notifications;
        }
 
        public async Task SeedAsync()
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods
    }
}