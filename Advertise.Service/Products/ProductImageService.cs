using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Products;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Managers.File;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Products;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Products
{
    public class ProductImageService : IProductImageService
    {
        #region Private Fields

        private readonly IMapper _mapper;
        private readonly IDbSet<ProductImage> _productImageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventPublisher _eventPublisher;

        #endregion Private Fields

        #region Public Constructors

        public ProductImageService(IMapper mapper, IUnitOfWork unitOfWork, IEventPublisher eventPublisher)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _eventPublisher = eventPublisher;
            _productImageRepository = unitOfWork.Set<ProductImage>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountAllByProductIdAsync(Guid productId)
        {
            var request = new ProductImageSearchModel
            {
                ProductId = productId
            };
            return await CountByRequestAsync(request);
        }

        public async Task<int> CountByRequestAsync(ProductImageSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var productImages = await QueryByRequest(model).CountAsync();

            return productImages;
        }

        public async Task CreateByViewModelAsync(ProductImageCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var productImage = _mapper.Map<ProductImage>(model);
            _productImageRepository.Add(productImage);

            await _unitOfWork.SaveAllChangesAsync();
            _eventPublisher.EntityInserted(productImage);
        }

        public async Task DeleteByIdAsync(Guid productImageId)
        {
            if (productImageId == null)
                throw new ArgumentNullException(nameof(productImageId));

            var productImage = await _productImageRepository.FirstOrDefaultAsync(model => model.Id == productImageId);
            _productImageRepository.Remove(productImage);

            await _unitOfWork.SaveAllChangesAsync();
            _eventPublisher.EntityDeleted(productImage);
        }

        public async Task EditByViewModelAsync(ProductImageEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var productImage = await _productImageRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            _mapper.Map(model, productImage);

            await _unitOfWork.SaveAllChangesAsync();
            _eventPublisher.EntityUpdated(productImage);
        }

        public async Task<ProductImage> FindByIdAsync(Guid productImageId)
        {
            if (productImageId == null)
                throw new ArgumentNullException(nameof(productImageId));

            return await _productImageRepository
                 .FirstOrDefaultAsync(model => model.Id == productImageId);
        }

        public IQueryable<ProductImage> QueryByRequest(ProductImageSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var productImages = _productImageRepository.AsNoTracking().AsQueryable()
                .Include(m => m.CreatedBy)
                .Include(m => m.CreatedBy.Meta)
                .Include(m => m.Product.Company);

            if (model.Term.HasValue())
                productImages = productImages.Where(m => m.Product.Company.Title.Contains(model.Term));
            if (model.ProductId.HasValue)
                productImages = productImages.Where(m => m.ProductId == model.ProductId);

            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;

            productImages = productImages.OrderBy($"{model.SortMember} {model.SortDirection}");

            return productImages;
        }

        public async Task<List<FileModel>> GetByProductIdAsFileModelAsync(Guid productId)
        {
            var request = new ProductImageSearchModel
            {
                ProductId = productId
            };
            var result = await ListByRequestAsync(request);
            return result.ProductImages.Select(s => new FileModel
            {
                Id = s.Id,
                Extension = Path.GetExtension(s.FileName),
                Name = s.FileName,
                Path = Path.Combine(FileConst.ImagesWebPath + s.FileName),
                Size = 0,
                Type = FileConst.FileType
            }).ToList();
        }

        public async Task<IList<ProductImage>> GetByProductIdAsync(Guid productId)
        {
            var requestImage = new ProductImageSearchModel
            {
                ProductId = productId,
                PageSize = PageSize.Count20
            };
            return await GetByRequestAsync(requestImage);
        }

        public async Task<IList<ProductImage>> GetByRequestAsync(ProductImageSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var productImages = await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);

            return productImages;
        }

        public async Task<ProductImageListModel> ListByRequestAsync(ProductImageSearchModel model)
        {
            model.TotalCount = await CountByRequestAsync(model);
            var images = await GetByRequestAsync(model);
            var imageViewModel = _mapper.Map<IList<ProductImageModel>>(images);
            return new ProductImageListModel
            {
                SearchModel = model,
                ProductImages = imageViewModel
            };
        }

        public async Task RemoveRangeAsync(IList<ProductImage> productImages)
        {
            if (productImages == null)
                throw new ArgumentNullException(nameof(productImages));

            foreach (var productImage in productImages)
                _productImageRepository.Remove(productImage);
        }

        #endregion Public Methods
    }
}