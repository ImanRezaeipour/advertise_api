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
    public class ProductCommentLikeService : IProductCommentLikeService
    {
        #region Private Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IDbSet<ProductCommentLike> _productCommentLikeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public ProductCommentLikeService(IMapper mapper, IUnitOfWork unitOfWork, IWebContextManager webContextManager, IEventPublisher eventPublisher)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _webContextManager = webContextManager;
            _eventPublisher = eventPublisher;
            _productCommentLikeRepository = unitOfWork.Set<ProductCommentLike>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(ProductCommentLikeSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).CountAsync();
        }

        public async Task<ProductCommentLike> FindByIdAsync(Guid productCommentId, Guid userId)
        {
            return await _productCommentLikeRepository
                 .FirstOrDefaultAsync(model => model.CommentId == productCommentId && model.LikedById == userId);
        }

        public async Task<IList<ProductCommentLike>> GetByRequestAsync(ProductCommentLikeSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var productCommentLikes = await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);

            return productCommentLikes;
        }

        public async Task<IList<User>> GetUsersByCompanyIdAsync(Guid questionId)
        {
            return await _productCommentLikeRepository
           .AsNoTracking()
           .Include(model => model.LikedById)
           .Where(model => model.CommentId == questionId && model.IsLike.GetValueOrDefault())
           .Select(model => model.LikedBy)
           .ToListAsync();
        }

        public async Task<bool> IsLikeCurrentUserByIdAsync(Guid productCommentId)
        {
            return await _productCommentLikeRepository
                 .AsNoTracking()
                 .AnyAsync(model => model.CommentId == productCommentId && model.LikedById == _webContextManager.CurrentUserId && model.IsLike.GetValueOrDefault());
        }

        public IQueryable<ProductCommentLike> QueryByRequest(ProductCommentLikeSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var productCommentLikes = _productCommentLikeRepository.AsNoTracking().AsQueryable()
                .Include(m => m.LikedBy)
                .Include(m => m.Comment);
            if (model.CommentId.HasValue)
                productCommentLikes = productCommentLikes.Where(m => m.CommentId == model.CommentId);
            if (model.LikedById.HasValue)
                productCommentLikes = productCommentLikes.Where(m => m.LikedById == model.LikedById);
            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;
            productCommentLikes = productCommentLikes.OrderBy($"{model.SortMember} {model.SortDirection}");

            return productCommentLikes;
        }

        public async Task SeedAsync()
        {
            throw new NotImplementedException();
        }

        public async Task ToggleCurrentUserByIdAsync(Guid productCommentId)
        {
            var productCommentLike = await FindByIdAsync(productCommentId, _webContextManager.CurrentUserId);
            if (productCommentLike != null)
            {
                productCommentLike.IsLike = productCommentLike.IsLike != null && !productCommentLike.IsLike.Value;
                await _unitOfWork.SaveAllChangesAsync();
                _eventPublisher.EntityUpdated(productCommentLike);
            }

            var newProductCommentLike = new ProductCommentLike
            {
                CommentId = productCommentId,
                IsLike = true,
                LikedById = _webContextManager.CurrentUserId
            };
            _productCommentLikeRepository.Add(newProductCommentLike);

            await _unitOfWork.SaveAllChangesAsync();
            _eventPublisher.EntityInserted(newProductCommentLike);
        }

        #endregion Public Methods
    }
}