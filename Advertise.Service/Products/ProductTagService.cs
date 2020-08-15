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
    public class ProductTagService : IProductTagService
    {
        #region Private Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IDbSet<ProductTag> _productTagRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public ProductTagService(IMapper mapper, IUnitOfWork unitOfWork, IEventPublisher eventPublisher)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _eventPublisher = eventPublisher;
            _productTagRepository = unitOfWork.Set<ProductTag>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountAllByProductIdAsync(Guid productId)
        {
            var request = new ProductTagSearchModel
            {
                ProductId = productId,
                SortDirection = SortDirection.Desc,
                SortMember = SortMember.CreatedOn,
                PageSize = PageSize.Count10,
            };
            return await CountByRequestAsync(request);
        }

        public async Task<int> CountByRequestAsync(ProductTagSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).CountAsync();
        }

        public async Task CreateByViewModelAsync(ProductTagCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var productTag = _mapper.Map<ProductTag>(model);
            _productTagRepository.Add(productTag);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityInserted(productTag);
        }

        public async Task DeleteByIdAsync(Guid productTagId)
        {
            var productTag = await _productTagRepository.FirstOrDefaultAsync(model => model.Id == productTagId);
            _productTagRepository.Remove(productTag);

            await _unitOfWork.SaveAllChangesAsync();
            _eventPublisher.EntityDeleted(productTag);
        }

        public async Task EditByViewModelAsync(ProductTagEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var productTag = await _productTagRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            _mapper.Map(model, productTag);

            await _unitOfWork.SaveAllChangesAsync();
            _eventPublisher.EntityUpdated(productTag);
        }

        public async Task<ProductTag> FindByIdAsync(Guid productTagId)
        {
            return await _productTagRepository
                 .FirstOrDefaultAsync(model => model.Id == productTagId);
        }

        public async Task<IList<ProductTag>> GetByProductIdAsync(Guid productId)
        {
            var requestTag = new ProductTagSearchModel
            {
                SortDirection = SortDirection.Desc,
                SortMember = SortMember.CreatedOn,
                ProductId = productId,
                PageSize = PageSize.Count10,
                PageIndex = 1
            };
            return await GetByRequestAsync(requestTag);
        }

        public async Task<IList<ProductTag>> GetByRequestAsync(ProductTagSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public async Task<ProductTagListModel> GetTagsByProductIdAsync(Guid productId)
        {
            var request = new ProductTagSearchModel
            {
                ProductId = productId
            };
            return await ListByRequestAsync(request);
        }

        public async Task<ProductTagListModel> ListByRequestAsync(ProductTagSearchModel model)
        {
            model.TotalCount = await CountByRequestAsync(model);
            var productTags = await GetByRequestAsync(model);
            var productTagViewModel = _mapper.Map<IList<ProductTagModel>>(productTags);
            return new ProductTagListModel
            {
                SearchModel = model,
                ProductTags = productTagViewModel
            };
        }

        public IQueryable<ProductTag> QueryByRequest(ProductTagSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var productTags = _productTagRepository.AsNoTracking().AsQueryable();
            if (model.ProductId.HasValue)
                productTags = productTags.Where(m => m.ProductId == model.ProductId);
           if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;
            productTags = productTags.OrderBy($"{model.SortMember} {model.SortDirection}");

            return productTags;
        }

        public async Task RemoveRangeAsync(IList<ProductTag> productTags)
        {
            if (productTags == null)
                throw new ArgumentNullException(nameof(productTags));

            foreach (var productTag in productTags)
                _productTagRepository.Remove(productTag);
        }

        public async Task SeedAsync()
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods
    }
}