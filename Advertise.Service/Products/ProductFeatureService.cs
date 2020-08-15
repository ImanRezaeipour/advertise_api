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
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Products;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Products
{
    public class ProductFeatureService : IProductFeatureService
    {
        #region Private Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IDbSet<ProductFeature> _productFeatureRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public ProductFeatureService(IMapper mapper, IUnitOfWork unitOfWork, IEventPublisher eventPublisher)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _eventPublisher = eventPublisher;
            _productFeatureRepository = unitOfWork.Set<ProductFeature>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(ProductFeatureSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).CountAsync();
        }

        public async Task CreateByViewModelAsync(ProductFeatureCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var productFeature = _mapper.Map<ProductFeature>(model);
            _productFeatureRepository.Add(productFeature);

            await _unitOfWork.SaveAllChangesAsync();
            _eventPublisher.EntityInserted(productFeature);
        }
        
        public async Task DeleteByIdAsync(Guid productFeatureId)
        {
            if (productFeatureId == null)
                throw new ArgumentNullException(nameof(productFeatureId));

            var productFeature = await _productFeatureRepository.FirstOrDefaultAsync(model => model.Id == productFeatureId);
            _productFeatureRepository.Remove(productFeature);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(productFeature);
        }

        public async Task EditByViewModelAsync(ProductFeatureEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var productFeature = await _productFeatureRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            _mapper.Map(model, productFeature);

            await _unitOfWork.SaveAllChangesAsync();
            _eventPublisher.EntityUpdated(productFeature);
        }

        public async Task<ProductFeature> FindByIdAsync(Guid productFeatureId)
        {
            if (productFeatureId == null)
                throw new ArgumentNullException(nameof(productFeatureId));

            return await _productFeatureRepository
                 .FirstOrDefaultAsync(model => model.Id == productFeatureId);
        }

        public async Task<IList<ProductFeature>> GetByRequestAsync(ProductFeatureSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public async Task<ProductFeatureListModel> ListByRequestAsync(ProductFeatureSearchModel model)
        {
            model.TotalCount = await CountByRequestAsync(model);
            var productComments = await GetByRequestAsync(model);
            var productCommentViewModel = _mapper.Map<IList<ProductFeatureModel>>(productComments);
            return new ProductFeatureListModel
            {
                SearchModel = model,
                ProductFeatures = productCommentViewModel
            };
        }

        public IQueryable<ProductFeature> QueryByRequest(ProductFeatureSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var productFeatures = _productFeatureRepository.AsNoTracking().AsQueryable();
            if (model.ProductId.HasValue)
                productFeatures = productFeatures.Where(m => m.ProductId == model.ProductId);
            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;
            productFeatures = productFeatures.OrderBy($"{model.SortMember} {model.SortDirection}");

            return productFeatures;
        }

        public async Task RemoveRangeAsync(IList<ProductFeature> productFeatures)
        {
            if (productFeatures == null)
                throw new ArgumentNullException(nameof(productFeatures));

            foreach (var productFeature in productFeatures)
                _productFeatureRepository.Remove(productFeature);
        }

        public async Task SeedAsync()
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods
    }
}