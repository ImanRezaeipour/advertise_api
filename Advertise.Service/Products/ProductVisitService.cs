using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Products;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Products;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Products
{
    public class ProductVisitService : IProductVisitService
    {
        #region Private Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IDbSet<ProductVisit> _productVisitRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public ProductVisitService(IMapper mapper, IUnitOfWork unitOfWork, IWebContextManager webContextManager, IEventPublisher eventPublisher)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _webContextManager = webContextManager;
            _eventPublisher = eventPublisher;
            _productVisitRepository = unitOfWork.Set<ProductVisit>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountAllByProductIdAsync(Guid productId)
        {
            var request = new ProductVisitSearchModel
            {
                ProductId = productId,
            };
            return await CountByRequestAsync(request);
        }

        public async Task<int> CountByProductIdAsync(Guid productId)
        {
            if (productId == null)
                throw new ArgumentNullException(nameof(productId));

            var request = new ProductVisitSearchModel
            {
                PageSize = PageSize.All,
                SortDirection = SortDirection.Asc,
                SortMember = SortMember.CreatedOn,
                PageIndex = 1,
                ProductId = productId
            };
            return await CountByRequestAsync(request);
        }

        public async Task<int> CountByRequestAsync(ProductVisitSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).CountAsync();
        }

        public async Task CreateAsync(ProductVisit productVisit)
        {
            if (productVisit == null)
                throw new ArgumentNullException(nameof(productVisit));

            _productVisitRepository.Add(productVisit);

            await _unitOfWork.SaveAllChangesAsync();
            _eventPublisher.EntityInserted(productVisit);
        }
        
        public async Task CreateByProductIdAsync(Guid productId)
        {
            if (productId == null)
                throw new ArgumentNullException(nameof(productId));

            var productVisit = new ProductVisit
            {
                ProductId = productId,
                IsVisit = true,
                VisitedById = _webContextManager.CurrentUserId,
                CreatedOn = DateTime.Now
                
            };
            _productVisitRepository.Add(productVisit);

            await _unitOfWork.SaveAllChangesAsync();
            _eventPublisher.EntityInserted(productVisit);
        }

        public async Task<ProductVisit> FindByIdAsync(Guid productVisitId)
        {
            return await _productVisitRepository
                  .FirstOrDefaultAsync(model => model.Id == productVisitId);
        }

        public async Task<ProductVisit> FindByProductIdAsync(Guid productId)
        {
            return await _productVisitRepository.FirstOrDefaultAsync(model => model.ProductId == productId);
        }

        public async Task<IList<ProductVisit>> GetByRequestAsync(ProductVisitSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public async Task<List<Guid>> GetMostVisitProductIdAsync()
        {
            var mostVisits = await _productVisitRepository.AsNoTracking()
                .GroupBy(model => model.ProductId)
                .Select(model => new { Id = model.Key.ToString(), Count = model.Count() })
                .OrderByDescending(model => model.Count)
                .Take(15)
                .ToListAsync();

            return mostVisits.Select(model => new Guid(model.Id)).ToList();
        }

        public async Task<IList<Guid>> GetLastProductIdByCurrentUserAsync()
        {
            return await _productVisitRepository.AsNoTracking()
                .Where(model => model.VisitedById == _webContextManager.CurrentUserId)
                .OrderByDescending(model => model.CreatedOn)
                .Select(model => model.ProductId.Value)
                .Take(15)
                .ToListAsync();
        }

        public async Task<ProductVisitListModel> ListByRequestAsync(ProductVisitSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            if (isCurrentUser)
                model.CreatedById = _webContextManager.CurrentUserId;
            else if (userId != null)
                model.CreatedById = userId;
            else
                model.CreatedById = null;

            model.TotalCount = await CountByRequestAsync(model);
            var productVisit = await GetByRequestAsync(model);
            var productVisitViewModel = _mapper.Map<IList<ProductVisitModel>>(productVisit);
            return new ProductVisitListModel
            {
                SearchModel = model,
                ProductVisits = productVisitViewModel
            };
        }

        public IQueryable<ProductVisit> QueryByRequest(ProductVisitSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var productVisits = _productVisitRepository.AsNoTracking().AsQueryable();
            if (model.ProductId.HasValue)
                productVisits = productVisits.Where(m => m.ProductId == model.ProductId);
            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;
            productVisits = productVisits.OrderBy($"{model.SortMember} {model.SortDirection}");

            return productVisits;
        }

        public async Task RemoveRangeAsync(IList<ProductVisit> productVisits)
        {
            if (productVisits == null)
                throw new ArgumentNullException(nameof(productVisits));

            foreach (var productVisit in productVisits)
                _productVisitRepository.Remove(productVisit);
        }

        public async Task SeedAsync()
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods
    }
}