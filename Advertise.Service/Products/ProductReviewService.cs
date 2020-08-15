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
using Advertise.Core.Managers.Html;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Products;
using Advertise.Core.Types;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Products
{
    public class ProductReviewService : IProductReviewService
    {
        #region Private Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IDbSet<Product> _productRepository;
        private readonly IDbSet<ProductReview> _productReviewRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public ProductReviewService(IMapper mapper, IUnitOfWork unitOfWork, IWebContextManager webContextManager, IEventPublisher eventPublisher)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _webContextManager = webContextManager;
            _eventPublisher = eventPublisher;
            _productRepository = unitOfWork.Set<Product>();
            _productReviewRepository = unitOfWork.Set<ProductReview>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(ProductReviewSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).CountAsync();
        }

        public async Task CreateByViewModelAsync(ProductReviewCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var productReview = _mapper.Map<ProductReview>(model);
            productReview.Body = productReview.Body.ToSafeHtml();
            productReview.CreatedById = _webContextManager.CurrentUserId;
            _productReviewRepository.Add(productReview);

            await _unitOfWork.SaveAllChangesAsync();
            _eventPublisher.EntityInserted(productReview);
        }

        public async Task DeleteByIdAsync(Guid productReviewId)
        {
            var productReview = await _productReviewRepository.FirstOrDefaultAsync(model => model.Id == productReviewId);
            _productReviewRepository.Remove(productReview);

            await _unitOfWork.SaveAllChangesAsync();
            _eventPublisher.EntityDeleted(productReview);
        }

        public async Task EditApproveByViewModelAsync(ProductReviewEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var productReview = await _productReviewRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            productReview.ApprovedById = _webContextManager.CurrentUserId;
            productReview.State = StateType.Approved;
            _mapper.Map(model, productReview);

            await _unitOfWork.SaveAllChangesAsync();
            _eventPublisher.EntityUpdated(productReview);
        }

        public async Task EditByViewModelAsync(ProductReviewEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var productReview = await _productReviewRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            var companyMap = _mapper.Map(model, productReview);
            companyMap.Body = companyMap.Body.ToSafeHtml();

            await _unitOfWork.SaveAllChangesAsync();
            _eventPublisher.EntityUpdated(productReview);
        }

        public async Task EditRejectByViewModelAsync(ProductReviewEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var productReview = await _productReviewRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            productReview.State = StateType.Rejected;
            _mapper.Map(model, productReview);

            await _unitOfWork.SaveAllChangesAsync();
            _eventPublisher.EntityUpdated(productReview);
        }

        public async Task<ProductReview> FindByIdAsync(Guid productReviewId)
        {
            if (productReviewId == null)
                throw new ArgumentNullException(nameof(productReviewId));

            return await _productReviewRepository.FirstOrDefaultAsync(model => model.Id == productReviewId);
        }

        public async Task<IList<ProductReview>> GetByProductIdAsync(Guid productId)
        {
            var request = new ProductReviewSearchModel
            {
                ProductId = productId
            };
            return await GetByRequestAsync(request);
        }

        public async Task<IList<ProductReview>> GetByRequestAsync(ProductReviewSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public async Task<IList<SelectList>> GetProductIdAsync()
        {
            var list = await _productRepository
                .AsNoTracking()
                .Where(model => model.State == StateType.All)
                .ToListAsync();

            return list.Select(item => new SelectList
            {
                Text = item.Title,
                Value = item.Id.ToString()
            }).ToList();
        }

        public IQueryable<ProductReview> QueryByRequest(ProductReviewSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var productReviews = _productReviewRepository.AsNoTracking().AsQueryable()
                .Include(m => m.CreatedBy)
                .Include(m => m.CreatedBy.Meta)
                .Include(m => m.Product);
            if (model.ProductId.HasValue)
                productReviews = productReviews.Where(m => m.ProductId == model.ProductId);
            if (model.ProductCode.HasValue())
                productReviews = productReviews.Where(m => m.Product.Code == model.ProductCode);
            if (model.Term.HasValue())
                productReviews = productReviews.Where(m => m.Body.Contains(model.Term));
            if (model.IsActive.HasValue)
                productReviews = productReviews.Where(m => m.IsActive == model.IsActive);
            if (model.State.HasValue)
                productReviews = productReviews.Where(m => m.State == model.State);
            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;
            productReviews = productReviews.OrderBy($"{model.SortMember} {model.SortDirection}");

            return productReviews;
        }

        public async Task RemoveRangeAsync(IList<ProductReview> productReviews)
        {
            if (productReviews == null)
                throw new ArgumentNullException(nameof(productReviews));

            foreach (var productReview in productReviews)
                _productReviewRepository.Remove(productReview);
        }

        public async Task SeedAsync()
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods
    }
}