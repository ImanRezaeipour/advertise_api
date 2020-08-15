using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Carts;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Carts;
using Advertise.Core.Model.Common;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Carts
{
    public class CartService : ICartService
    {
        #region Private Fields

        private readonly IDbSet<Cart> _bagRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public CartService(IMapper mapper, IUnitOfWork unitOfWork, IWebContextManager webContextManager, IEventPublisher eventPublisher)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _webContextManager = webContextManager;
            _eventPublisher = eventPublisher;
            _bagRepository = unitOfWork.Set<Cart>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByCurrentUserAsync()
        {
            var request = new CartSearchModel
            {
                SortDirection = SortDirection.Desc,
                PageSize = PageSize.Count100,
                CreatedById = _webContextManager.CurrentUserId
            };
            return await CountByRequestAsync(request);
        }

        public async Task<int> CountByRequestAsync(CartSearchModel request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return await QueryByRequest(request).CountAsync();
        }

        public async Task CreateByIdAsync(Guid productId) 
        {
            var isBagExist = await FindByProductIdAsync(productId, _webContextManager.CurrentUserId);
            if (isBagExist == null)
            {

                var bag = new Cart
                {
                    ProductCount = 1,
                    ProductId = productId,
                    CreatedById = _webContextManager.CurrentUserId,
                    CreatedOn = DateTime.Now

                };
                _bagRepository.Add(bag);

                await _unitOfWork.SaveAllChangesAsync();

                _eventPublisher.EntityInserted(bag);
            }
        }

        public async Task DeleteByCurrentUserAsync()
        {
            var request = new CartSearchModel
            {
                CreatedById = _webContextManager.CurrentUserId
            };
            var bags = await GetByRequestAsync(request);
            foreach (var bag in bags)
            {
                _bagRepository.Attach(bag);
                _bagRepository.Remove(bag);


                _eventPublisher.EntityDeleted(bag);
            }
            await _unitOfWork.SaveAllChangesAsync();

        }

        public async Task DeleteByIdAsync(Guid bagId)
        {
            if (bagId == null)
                throw new ArgumentNullException(nameof(bagId));

            var bag = await FindByIdAsync(bagId);
            _bagRepository.Remove(bag);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(bag);
        }

        public async Task DeleteByProductIdAsync(Guid productId)
        {
            if (productId == null)
                throw new ArgumentNullException(nameof(productId));

            var bag = await FindByProductIdAsync(productId, _webContextManager.CurrentUserId);
            _bagRepository.Remove(bag);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(bag);
        }

        public async Task<Cart> FindByIdAsync(Guid bagId)
        {
            return await _bagRepository.SingleOrDefaultAsync(model => model.Id == bagId);
        }

        public async Task<Cart> FindByProductIdAsync(Guid productId, Guid userId)
        {
            return await _bagRepository
                .FirstOrDefaultAsync(model => model.CreatedById == userId && model.ProductId == productId);
        }

        public async Task<Cart> FindByUserIdAsync(Guid userId)
        {
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));

            return await _bagRepository.FirstOrDefaultAsync(model => model.CreatedById == userId);
        }

        public async Task<IList<Cart>> GetByRequestAsync(CartSearchModel request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return await QueryByRequest(request).ToPagedListAsync(request.PageIndex, request.PageSize);
        }

        public async Task<IList<Cart>> GetByUserIdAsync(Guid userId)
        {
            var request = new CartSearchModel
            {
                PageSize = PageSize.Count100,
                SortMember = SortMember.ModifiedOn,
                CreatedById = _webContextManager.CurrentUserId
            };
            return await GetByRequestAsync(request);
        }

        public async Task<bool> IsExistByProductIdAsync(Guid productId, Guid? userId = null)
        {
            var query = _bagRepository.AsNoTracking()
                .Where(model => model.ProductId == productId);
            if (userId != null)
                query = query.Where(model => model.CreatedById == userId);
            return await query.AnyAsync();
        }

        public IQueryable<Cart> QueryByRequest(CartSearchModel request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var bags = _bagRepository.AsNoTracking().AsQueryable();

            if (request.CreatedById.HasValue)
                bags = bags.Where(model => model.CreatedById == request.CreatedById);
            if (request.Term.HasValue())
                bags = bags.Where(model => model.CreatedById == request.CreatedById);

            if (string.IsNullOrEmpty(request.SortMember))
                request.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(request.SortDirection))
                request.SortDirection = SortDirection.Desc;

            bags = bags.OrderBy($"{request.SortMember} {request.SortDirection}");

            return bags;
        }

        public async Task RemoveRangeAsync(IList<Cart> bags)
        {
            if (bags == null)
                throw new ArgumentNullException(nameof(bags));

            foreach (var bag in bags)
            {
                _bagRepository.Remove(bag);

                await _unitOfWork.SaveAllChangesAsync();

                _eventPublisher.EntityDeleted(bag);
            }
        }

        public async Task SetProductCountByIdAsync(Guid productId, int productCount)
        {
            var bag = await FindByProductIdAsync(productId, _webContextManager.CurrentUserId);
            bag.ProductCount = productCount;

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(bag);
        }

        public async Task ToggleByCurrentUserAsync(Guid productId)
        {
            var bag = await FindByProductIdAsync(productId, _webContextManager.CurrentUserId);
            if (bag == null)
            {
                var newBag = new Cart
                {
                    ProductId = productId,
                    CreatedById = _webContextManager.CurrentUserId
                };
                _bagRepository.Add(newBag);

                await _unitOfWork.SaveAllChangesAsync();

                _eventPublisher.EntityInserted(newBag);
            }
            _bagRepository.Remove(bag);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(bag);
        }

        #endregion Public Methods
    }
}