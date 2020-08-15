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
using Advertise.Core.Types;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Products
{
    public class ProductCommentService : IProductCommentService
    {
        #region Private Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IDbSet<ProductComment> _productCommentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public ProductCommentService(IMapper mapper, IUnitOfWork unitOfWork, IWebContextManager webContextManager, IEventPublisher eventPublisher)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _webContextManager = webContextManager;
            _eventPublisher = eventPublisher;
            _productCommentRepository = unitOfWork.Set<ProductComment>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task EditApproveByViewModelAsync(ProductCommentEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var productComment = await FindByIdAsync(model.Id);
            _mapper.Map(model, productComment);
            productComment.State = StateType.Approved;
            productComment.ApprovedById = _webContextManager.CurrentUserId;

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(productComment);
        }

        public async Task<int> CountByRequestAsync(ProductCommentSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).CountAsync();
        }

        public async Task CreateByViewModelAsync(ProductCommentCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var productComment = _mapper.Map<ProductComment>(model);
            productComment.State = StateType.Pending;
            productComment.CreatedOn = DateTime.Now;
            productComment.CommentedById = _webContextManager.CurrentUserId;
            _productCommentRepository.Add(productComment);

            await _unitOfWork.SaveAllChangesAsync();
            _eventPublisher.EntityInserted(productComment);
        }

        public async Task DeleteByIdAsync(Guid productCommentId, bool isCurrentUser = false)
        {
            if (productCommentId == null)
                throw new ArgumentNullException(nameof(productCommentId));

            var productComment = await FindByIdAsync(productCommentId);
            if(isCurrentUser)
                return;

            _productCommentRepository.Remove(productComment);

            await _unitOfWork.SaveAllChangesAsync();
            _eventPublisher.EntityDeleted(productComment);
        }

        public async Task EditByViewModelAsync(ProductCommentEditModel model, bool isCurrentUser = false)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var productComment = await _productCommentRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            if(isCurrentUser)
                return;

            _mapper.Map(model, productComment);

            await _unitOfWork.SaveAllChangesAsync();
            _eventPublisher.EntityUpdated(productComment);
        }

        public async Task<ProductComment> FindByIdAsync(Guid productCommentId)
        {
            return await _productCommentRepository
                .FirstOrDefaultAsync(model => model.Id == productCommentId);
        }

        public async Task<IList<ProductComment>> GetByRequestAsync(ProductCommentSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public async Task<ProductCommentListModel> ListByRequestAsync(ProductCommentSearchModel model)
        {
            model.TotalCount = await CountByRequestAsync(model);
            var productComments = await GetByRequestAsync(model);
            var productCommentsViewModel = _mapper.Map<IList<ProductCommentModel>>(productComments);
            return new ProductCommentListModel
            {
                SearchModel = model,
                ProductComments = productCommentsViewModel
            };
        }

        public IQueryable<ProductComment> QueryByRequest(ProductCommentSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var productComments = _productCommentRepository.AsNoTracking().AsQueryable()
                .Include(m => m.Product)
                .Include(m => m.Product.Company);
            if (model.State.HasValue)
                if (model.State != StateType.All)
                    productComments = productComments.Where(productComment => productComment.State == model.State);
            if (model.CommentedById.HasValue)
                productComments = productComments.Where(m => m.CommentedById == model.CommentedById);
            if (model.ProductId.HasValue)
                productComments = productComments.Where(m => m.ProductId == model.ProductId);
            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;
            productComments = productComments.OrderBy($"{model.SortMember} {model.SortDirection}");

            return productComments;
        }

        public async Task EditRejectByViewModelAsync(ProductCommentEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var productComment = await FindByIdAsync(model.Id);
            _mapper.Map(model, productComment);
            productComment.State = StateType.Rejected;

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(productComment);
        }

        public async Task RemoveRangeAsync(IList<ProductComment> productComments)
        {
            if (productComments == null)
                throw new ArgumentNullException(nameof(productComments));

            foreach (var productComment in productComments)
            {
                _productCommentRepository.Remove(productComment);
            }

            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task SetStateByIdAsync(Guid productCommentId, StateType state)
        {
            var productComment = await FindByIdAsync(productCommentId);
            productComment.State = state;

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(productComment);
        }

        #endregion Public Methods
    }
}