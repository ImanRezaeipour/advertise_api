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
    public class ProductSpecificationService : IProductSpecificationService
    {
        #region Private Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IDbSet<ProductSpecification> _productSpecificationRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public ProductSpecificationService(IMapper mapper, IUnitOfWork unitOfWork, IEventPublisher eventPublisher)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _eventPublisher = eventPublisher;
            _productSpecificationRepository = unitOfWork.Set<ProductSpecification>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(ProductSpecificationSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).CountAsync();
        }

        public async Task CreateByViewModelAsync(ProductSpecificationCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var productSpecification = _mapper.Map<ProductSpecification>(model);
            _productSpecificationRepository.Add(productSpecification);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityInserted(productSpecification);
        }

        public async Task DeleteByIdAsync(Guid productSpecId)
        {
            var productSpec = await _productSpecificationRepository.FirstOrDefaultAsync(model => model.Id == productSpecId);
            _productSpecificationRepository.Remove(productSpec);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(productSpec);
        }

        public async Task EditByViewModelAsync(ProductSpecificationEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var productSpecification = await _productSpecificationRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            _mapper.Map(model, productSpecification);

            await _unitOfWork.SaveAllChangesAsync();
            _eventPublisher.EntityUpdated(productSpecification);
        }

        public async Task<ProductSpecification> FindByIdAsync(Guid productSpecificationId)
        {
            return await _productSpecificationRepository.FirstOrDefaultAsync(model => model.Id == productSpecificationId);
        }

        public async Task<IList<ProductSpecification>> GetByProductIdAsync(Guid productId)
        {
            var specificationRequest = new ProductSpecificationSearchModel
            {
                SortDirection = SortDirection.Asc,
                SortMember = SortMember.CreatedOn,
                PageSize = PageSize.Count100,
                PageIndex = 1,
                ProductId = productId
            };
            return await GetByRequestAsync(specificationRequest);
        }

        public async Task<IList<ProductSpecification>> GetByRequestAsync(ProductSpecificationSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public IQueryable<ProductSpecification> QueryByRequest(ProductSpecificationSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var productSpecifications = _productSpecificationRepository.AsNoTracking().AsQueryable()
                .Include(m => m.Product)
                .Include(m => m.Specification)
                .Include(m => m.SpecificationOption);
            if (model.ProductId.HasValue)
                productSpecifications = productSpecifications.Where(m => m.ProductId == model.ProductId);
               if (model.Term.HasValue())
                productSpecifications = productSpecifications.Where(m => m.Value.Contains(model.Term));
            productSpecifications = productSpecifications.OrderBy($"{model.SortMember ?? SortMember.CreatedOn} {model.SortDirection ?? SortDirection.Asc}");

            return productSpecifications;
        }

        public async Task RemoveRangeAsync(IList<ProductSpecification> productSpecifications)
        {
            if (productSpecifications == null)
                throw new ArgumentNullException(nameof(productSpecifications));

            foreach (var productSpecification in productSpecifications)
                _productSpecificationRepository.Remove(productSpecification);
        }

        public async Task SeedAsync()
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods
    }
}