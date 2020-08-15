using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Products;
using Advertise.Core.Exceptions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Products;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Products
{
    public class ProductRateService : IProductRateService
    {
        #region Private Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IDbSet<ProductRate> _productRateRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public ProductRateService(IEventPublisher eventPublisher, IMapper mapper, IUnitOfWork unitOfWork, IWebContextManager webContextManager)
        {
            _productRateRepository = unitOfWork.Set<ProductRate>();
            _eventPublisher = eventPublisher;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _webContextManager = webContextManager;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(ProductRateSearchModel model)
        {
            return await QueryByRequest(model).CountAsync();
        }

        public async Task CreateByViewModelAsync(ProductRateCreateModel model)
        {
            if (model.ProductId != null)
            {
                var isRated = await IsRatedCurrentUserByProductAsync(model.ProductId.Value);
                if (isRated)
                    throw new JsonValidationException("شما قبلا رای خود را داده اید");
            }

            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var productRate = _mapper.Map<ProductRate>(model);
            productRate.CreatedById = _webContextManager.CurrentUserId;
            _productRateRepository.Add(productRate);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityInserted(productRate);
        }

        public async Task<decimal> GetRateByProductIdAsync(Guid productId)
        {
            var ratesSum = await _productRateRepository.AsNoTracking()
                .Where(model => model.ProductId == productId).SumAsync(model => (int?)model.Rate) ?? 0;

            var ratesCount = await _productRateRepository.AsNoTracking()
                .Where(model => model.ProductId == productId).CountAsync();

            if (ratesCount == 0)
                return 0;

            return ratesSum.ToDecimal() / ratesCount.ToDecimal();
        }

        public async Task<int> GetUserCountByProductIdAsync(Guid productId)
        {
            var request = new ProductRateSearchModel
            {
                ProductId = productId
            };

            return await QueryByRequest(request).CountAsync();
        }

        public async Task<int> GetRateByCurrentUserAsync(Guid productId)
        {
            return (await _productRateRepository.AsNoTracking()
                .Where(model =>model.CreatedById == _webContextManager.CurrentUserId && model.ProductId == productId)
                .Select(model => model.Rate).SingleOrDefaultAsync()).ToInt32();
        }

        public async Task<bool> IsRatedCurrentUserByProductAsync(Guid productId)
        {
            return await _productRateRepository.AsNoTracking()
                .AnyAsync(model => model.ProductId == productId && model.CreatedById == _webContextManager.CurrentUserId);
        }

        public IQueryable<ProductRate> QueryByRequest(ProductRateSearchModel model)
        {
            var productRates = _productRateRepository.AsNoTracking().AsQueryable();

            if (model.CreatedById != null)
                productRates = productRates.Where(m => m.CreatedById == model.CreatedById);
            if (model.ProductId != null)
                productRates = productRates.Where(m => m.ProductId == model.ProductId);

            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Asc;

            productRates = productRates.OrderBy($"{model.SortMember} {model.SortDirection}");

            return productRates;
        }

        public async Task<decimal> RateByRequestAsync(ProductRateSearchModel model)
        {
            var rates = await QueryByRequest(model).SumAsync(m => m.Rate.ToInt32());
            var counts = await QueryByRequest(model).CountAsync();
            var rate = rates / counts;

            return rate;
        }

        #endregion Public Methods
    }
}