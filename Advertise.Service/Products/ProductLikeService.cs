using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Products;
using Advertise.Core.Domain.Users;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Products;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Products
{
    public class ProductLikeService : IProductLikeService
    {
        #region Private Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IDbSet<ProductLike> _productLikeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public ProductLikeService(IMapper mapper, IUnitOfWork unitOfWork, IWebContextManager webContextManager, IEventPublisher eventPublisher)
        {
            _unitOfWork = unitOfWork;
            _webContextManager = webContextManager;
            _eventPublisher = eventPublisher;
            _productLikeRepository = unitOfWork.Set<ProductLike>();
            _mapper = mapper;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountAllLikedByProductIdAsync(Guid productId)
        {
            var request = new ProductLikeSearchModel
            {
                ProductId = productId,
                IsLike = true
            };
           return await CountByRequestAsync(request);
        }

        public async Task<int> CountByProductIdAsync(Guid productId)
        {
            var request = new ProductLikeSearchModel
            {
                PageSize = PageSize.All,
                ProductId = productId
            };
           return await CountByRequestAsync(request);
        }

        public async Task<List<ProductLikeModel>> GetByProductsAsync(List<Guid> productsId)
        {
            var listProductLike = await _productLikeRepository
                .Where(model => productsId.Contains((Guid)model.ProductId) && model.IsLike == true)
                .Distinct()
                .ToListAsync();
           return  _mapper.Map<List<ProductLikeModel>>(listProductLike);
        }

        public async Task<int> CountByRequestAsync(ProductLikeSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

           return await  QueryByRequest(model).CountAsync();
        }

        public async Task  CreateByViewModelAsync(ProductLikeCreateModel model)
        {
            var productLike = _mapper.Map<ProductLike>(model);
            productLike.LikedById = _webContextManager.CurrentUserId;
            _productLikeRepository.Add(productLike);

            await _unitOfWork.SaveAllChangesAsync();
            _eventPublisher.EntityInserted(productLike);
        }

        public async Task RemoveRangeByproductLikesAsync(IList<ProductLike> productLikes)
        {
            if (productLikes == null)
                throw new ArgumentNullException(nameof(productLikes));

            foreach (var productLike in productLikes)
                _productLikeRepository.Remove(productLike);
        }

        public async Task  DeleteByIdAsync(Guid productLikeId)
        {
            var productLike = await _productLikeRepository.FirstOrDefaultAsync(model => model.Id == productLikeId);
            _productLikeRepository.Remove(productLike);

             await _unitOfWork.SaveAllChangesAsync();
            _eventPublisher.EntityDeleted(productLike);
        }

        public async Task  EditByViewModelAsync(ProductLikeEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var productLike = await _productLikeRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            _mapper.Map(model, productLike);

            await _unitOfWork.SaveAllChangesAsync();
            _eventPublisher.EntityUpdated(productLike);
        }

        public async Task<ProductLike> FindByIdAsync(Guid productLikeId)
        {
           return  await _productLikeRepository.FirstOrDefaultAsync(model => model.Id == productLikeId);
        }

        public async Task<ProductLike> FindByProductIdAsync( Guid productId, Guid? userId =null)
        {
            var query = _productLikeRepository.AsQueryable();
            query = query.Where(model => model.ProductId == productId);

            if (userId.HasValue)
                query = query.Where(model => model.LikedById == userId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IList<ProductLike>> GetByRequestAsync(ProductLikeSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return  await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public async Task<List<Guid>> GetMostLikedProductIdAsync()
        {
            var mostLikeds = await _productLikeRepository
                .AsNoTracking()
                .GroupBy(model => model.ProductId)
                .Select(model => new
                {
                    Id = model.Key.ToString(),
                    Count = model.Count()
                })
                .OrderByDescending(model => model.Count)
                .Take(15)
                .ToListAsync();
            return mostLikeds.Select(model => new Guid(model.Id)).ToList();
            
        }

        public async Task<IList<User>> GetUsersByProductAsync(Guid productId)
        {
         return  await _productLikeRepository
                .Include(model => model.LikedById)
                .AsNoTracking()
                .Where(model => model.ProductId == productId && model.IsLike == true)
                .Select(model => model.LikedBy)
                .ToListAsync();
        }

        public async Task<bool> IsLikeCurrentUserByProductIdAsync(Guid productId)
        {
           return  await _productLikeRepository
                .AsNoTracking()
                .AnyAsync(model => model.ProductId == productId && model.LikedById == _webContextManager.CurrentUserId && model.IsLike == true);
        }

        public async Task<bool> IsLikeByProductIdAsync(Guid productId, Guid userId)
        {
           return  await _productLikeRepository
                .AsNoTracking()
                .AnyAsync(model => model.ProductId == productId && model.LikedById == userId && model.IsLike.GetValueOrDefault());
        }

        public IQueryable<ProductLike> QueryByRequest(ProductLikeSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var productLikes = _productLikeRepository.AsNoTracking().AsQueryable()
                .Include(m => m.LikedBy)
                .Include(m => m.LikedBy.Meta)
                .Include(m => m.Product)
                .Distinct();
            if (model.CompanyId.HasValue)
                productLikes = productLikes.Where(m => m.Product.CompanyId == model.CompanyId);
            if (model.ProductId.HasValue)
                productLikes = productLikes.Where(m => m.ProductId == model.ProductId);
            if (model.LikedById.HasValue)
                productLikes = productLikes.Where(m => m.LikedById == model.LikedById);
            if (model.IsLike.HasValue)
                productLikes = productLikes.Where(m => m.IsLike == model.IsLike);

            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;

            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;
            productLikes = productLikes.OrderBy($"{model.SortMember} {model.SortDirection}");

            return productLikes;
        }

        public async Task  RemoveRangeAsync(IList<ProductLike> productLikes)
        {
            if (productLikes == null)
                throw new ArgumentNullException(nameof(productLikes));

            foreach (var productLike in productLikes)
                _productLikeRepository.Remove(productLike);
        }

        public async Task  SeedAsync()
        {
            throw new NotImplementedException();
        }

        public async Task ToggleCurrentUserByProductIdAsync(Guid productId)
        {
            var productLike = await FindByProductIdAsync(productId, _webContextManager.CurrentUserId);
            if (productLike != null)
            {
                productLike.IsLike = !productLike.IsLike.GetValueOrDefault();

                await _unitOfWork.SaveAllChangesAsync();
                _eventPublisher.EntityUpdated(productLike);
            }

            else
            {
                var newProductLike = new ProductLike
                {
                    ProductId = productId,
                    IsLike = true,
                    LikedById = _webContextManager.CurrentUserId
                };
                _productLikeRepository.Add(newProductLike);

                await _unitOfWork.SaveAllChangesAsync();
                _eventPublisher.EntityInserted(newProductLike);
            }
        }

        #endregion Public Methods
    }
}