using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Products;
using Advertise.Core.Managers.Event;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Products;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Products
{
    public class ProductNotifyService : IProductNotifyService
    {
        #region Private Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IDbSet<ProductNotify> _productNotifyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public ProductNotifyService(IUnitOfWork unitOfWork, IMapper mapper, IEventPublisher eventPublisher, IWebContextManager webContextManager)
        {
            _unitOfWork = unitOfWork;
            _productNotifyRepository = unitOfWork.Set<ProductNotify>();
            _mapper = mapper;
            _eventPublisher = eventPublisher;
            _webContextManager = webContextManager;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(ProductNotifySearchModel model)
        {
            return await QueryByRequest(model).CountAsync();
        }

        public async Task CreateByViewModelAsync(ProductNotifyModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var productNotify = _mapper.Map<ProductNotify>(model);
            productNotify.CreatedById = _webContextManager.CurrentUserId;
            _productNotifyRepository.Add(productNotify);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityInserted(productNotify);
        }

        public async Task DeleteByProductIdAsync(Guid productId, bool? applyCurrentUser = false)
        {
            if (productId == null)
                throw new ArgumentNullException(nameof(productId));

            var productNotify = await FindByProductIdAync(productId, applyCurrentUser);
            _productNotifyRepository.Remove(productNotify);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(productNotify);
        }

        public async Task<ProductNotify> FindByProductIdAync(Guid productId, bool? applyCurrentUser = false)
        {
            var productNotifyQuery = _productNotifyRepository.AsQueryable();
            if (applyCurrentUser == true)
                productNotifyQuery = productNotifyQuery.Where(model => model.CreatedById == _webContextManager.CurrentUserId);

            return await productNotifyQuery.FirstOrDefaultAsync(model => model.ProductId == productId);
        }

        public async Task<IList<Guid?>> GetUsersByProductIdAsync(Guid productId)
        {
            if (productId == null)
                throw new ArgumentNullException(nameof(productId));

            return await _productNotifyRepository
                  .AsNoTracking()
                  .Where(model => model.ProductId == productId)
                  .Select(model => model.CreatedById)
                  .ToListAsync();
        }

        public async Task<bool> IsExistAsync(Guid productId, Guid userId)
        {
            return await _productNotifyRepository.AnyAsync(model => model.ProductId == productId && model.CreatedById == userId);
        }

        public async Task<bool> IsExistByProductIdAsync(Guid productId, bool? applyCurrentUser)
        {
            var query = _productNotifyRepository.AsQueryable();
            if (applyCurrentUser == true)
                query = query.Where(model => model.CreatedById == _webContextManager.CurrentUserId);
            return await query.AnyAsync(model => model.ProductId == productId);
        }

        public IQueryable<ProductNotify> QueryByRequest(ProductNotifySearchModel model)
        {
            var productNotifies = _productNotifyRepository.AsNoTracking().AsQueryable();

            if (model.CreatedById != null)
                productNotifies = productNotifies.Where(m => m.CreatedById == model.CreatedById);
            if (model.ProductId != null)
                productNotifies = productNotifies.Where(m => m.ProductId == model.ProductId);

            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Asc;

            productNotifies = productNotifies.OrderBy($"{model.SortMember} {model.SortDirection}");

            return productNotifies;
        }

        public async Task ToggleByProductIdAsync(Guid productId)
        {
            var isExistNotify = await IsExistByProductIdAsync(productId, true);
            if (isExistNotify)
            {
                await DeleteByProductIdAsync(productId, true);
            }
            else
            {
                var viewModel = new ProductNotifyModel
                {
                    ProductId = productId
                };
                await CreateByViewModelAsync(viewModel);
            }
        }

        #endregion Public Methods
    }
}