using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using Advertise.Core.Common;
using Advertise.Core.Domain.Catalogs;
using Advertise.Core.Domain.Categories;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Domain.Locations;
using Advertise.Core.Domain.Products;
using Advertise.Core.Domain.Users;
using Advertise.Core.Extensions;
using Advertise.Core.Helpers;
using Advertise.Core.Managers.Event;
using Advertise.Core.Managers.File;
using Advertise.Core.Managers.Html;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Products;
using Advertise.Core.Objects;
using Advertise.Core.Types;
using Advertise.Data.DbContexts;
using Advertise.Service.Carts;
using Advertise.Service.Categories;
using Advertise.Service.Keywords;
using Advertise.Service.Locations;
using Advertise.Service.Specifications;
using Advertise.Service.Users;
using AutoMapper;
using LinqKit;
using Z.EntityFramework.Plus;

namespace Advertise.Service.Products
{
    public class ProductService : IProductService
    {
        #region Private Fields

        private readonly ICartService _bagService;
        private readonly IDbSet<Category> _categoryRepository;
        private readonly IDbSet<Company> _companyRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IKeywordService _keywordService;
        private readonly ILocationCityService _cityService;
        private readonly ISpecificationOptionService _specificationOptionService;
        private readonly ISpecificationService _specificationService;
        private readonly IMapper _mapper;
        private readonly IUserNotificationService _notificationService;
        private readonly IProductCommentService _productCommentService;
        private readonly IDbSet<ProductFeature> _productFeatureRepository;
        private readonly IProductFeatureService _productFeatureService;
        private readonly IDbSet<ProductImage> _productImageRepository;
        private readonly IProductImageService _productImageService;
        private readonly IDbSet<ProductKeyword> _productKeywordRepository;
        private readonly IProductLikeService _productLikeService;
        private readonly IDbSet<Product> _productRepository;
        private readonly IProductReviewService _productReviewService;
        private readonly IDbSet<ProductSpecification> _productSpecificationRepository;
        private readonly IProductSpecificationService _productSpecificationService;
        private readonly IDbSet<ProductTag> _productTagRepository;
        private readonly IProductTagService _productTagService;
        private readonly IProductVisitService _productVisitService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<User> _userRepository;
        private readonly IWebContextManager _webContextManager;
        private readonly IDbSet<Catalog> _catalogRepository;

        #endregion Private Fields

        #region Public Constructors

        public ProductService(IMapper mapper, IProductReviewService productReviewService, IProductImageService productImageService, IProductTagService productTagService, IProductFeatureService productFeatureService, IProductVisitService productVisitService, IProductSpecificationService productSpecificationService, ICartService bagService, IProductCommentService productCommentService, IUnitOfWork unitOfWork, IWebContextManager webContextManager, IEventPublisher eventPublisher, IUserNotificationService notificationService, IKeywordService keywordService, IProductLikeService productLikeService, ISpecificationOptionService specificationOptionService, ISpecificationService specificationService, ILocationCityService cityService)
        {
            _mapper = mapper;
            _productImageRepository = unitOfWork.Set<ProductImage>();
            _productImageService = productImageService;
            _productTagService = productTagService;
            _productFeatureService = productFeatureService;
            _productCommentService = productCommentService;
            _productVisitService = productVisitService;
            _productSpecificationService = productSpecificationService;
            _bagService = bagService;
            _productCommentService = productCommentService;
            _productRepository = unitOfWork.Set<Product>();
            _productFeatureRepository = unitOfWork.Set<ProductFeature>();
            _productTagRepository = unitOfWork.Set<ProductTag>();
            _productSpecificationRepository = unitOfWork.Set<ProductSpecification>();
            _unitOfWork = unitOfWork;
            _webContextManager = webContextManager;
            _eventPublisher = eventPublisher;
            _notificationService = notificationService;
            _keywordService = keywordService;
            _productLikeService = productLikeService;
            _specificationOptionService = specificationOptionService;
            _specificationService = specificationService;
            _cityService = cityService;
            _catalogRepository = unitOfWork.Set<Catalog>();
            _productKeywordRepository = unitOfWork.Set<ProductKeyword>();
            _categoryRepository = unitOfWork.Set<Category>();
            _userRepository = unitOfWork.Set<User>();
            _companyRepository = unitOfWork.Set<Company>();
            _productReviewService = productReviewService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<decimal?> AverageByRequestAsync(ProductSearchModel model, string aggregateMember)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var products = QueryByRequest(model);
            switch (aggregateMember)
            {
                case "Price":
                    var memberAverage = await products.AverageAsync(m => m.Price);
                    return memberAverage;
            }

            return null;
        }

        public async Task<bool> CompareCodeAndSlugAsync(string code, string slug)
        {
            if (string.IsNullOrEmpty(code))
                return false;

            if (string.IsNullOrEmpty(slug))
                return true;

            var product = await FindByCodeAsync(code);
            return product.Title.CastToSlug() == slug;
        }

        public async Task<bool> IsApprovedAsync(string code)
        {
            return await _productRepository.AnyAsync(model => model.Code == code && model.State == StateType.Approved);
        }

        public async Task<int> CountAllAsync()
        {
            var request = new ProductSearchModel();
            return await CountByRequestAsync(request);
        }

        public async Task<int> CountByStateAsync(StateType productState)
        {
            var request = new ProductSearchModel
            {
                State = productState
            };
            return await CountByRequestAsync(request);
        }

        public async Task<int> CountByCategoryIdAsync(Guid categoryId)
        {
            var request = new ProductSearchModel
            {
                CategoryId = categoryId
            };
            return await CountByRequestAsync(request);
        }

        public async Task<int> CountByCompanyIdAsync(Guid companyId, StateType? state = null)
        {
            var request = new ProductSearchModel
            {
                CompanyId = companyId,
                State = state
            };
            return await CountByRequestAsync(request);
        }

        public async Task<int> CountByRequestAsync(ProductSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (isCurrentUser)
                model.UserId = _webContextManager.CurrentUserId;
            else if (userId != null)
                model.UserId = userId;
            else model.UserId = null;

            if (model.DistinctByCompanyId == true)
                return await QueryByRequest(model).GroupBy(m => m.CompanyId).CountAsync();
            return await QueryByRequest(model).CountAsync();
        }

        public async Task<int> CountByUserIdAsync(Guid userId, StateType state)
        {
            var request = new ProductSearchModel
            {
                State = state
            };
            return await CountByRequestAsync(request, userId: userId);
        }

        public async Task CreateByViewModelAsync(ProductCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var ownedBy = await _userRepository.FirstOrDefaultAsync(m => m.Id == _webContextManager.CurrentUserId);
            var company = await _companyRepository.FirstOrDefaultAsync(m => m.CreatedById == _webContextManager.CurrentUserId);

            var product = _mapper.Map<Product>(model);
            product.Description = model.Description.ToSafeHtml();
            product.Location = null;
            product.CreatedById = ownedBy.Id;
            product.CompanyId = company.Id;
            product.CategoryId = model.CategoryId != Guid.Empty ? model.CategoryId : (await _categoryRepository.FirstOrDefaultAsync(m => m.ParentId == null)).Id;
            product.Code = await GenerateCodeAsync();
            product.State = StateType.Pending;
            product.CreatedOn = DateTime.Now;

            var keywords = model.ProductKeywords;
            var productKeywords = new List<ProductKeyword>();
            if (keywords != null)
            {
                foreach (var keyword in keywords)
                {
                    var productKeyword = new ProductKeyword();
                    Guid isGuid;
                    Guid.TryParse(keyword, out isGuid);
                    if (isGuid != Guid.Empty)
                        productKeyword.KeywordId = keyword.ToGuid();
                    else
                        productKeyword.Title = keyword;

                    productKeywords.Add(productKeyword);
                }
            }
            product.ProductKeywords = productKeywords;

            var features = _mapper.Map<List<ProductFeature>>(model.Features);
            product.Features = features.Where(feature => !string.IsNullOrEmpty(feature.Title?.Trim())).ToList();

            var images = _mapper.Map<List<ProductImage>>(model.Images);
            product.Images = images;

            var tags = _mapper.Map<List<ProductTag>>(model.ProductTags);
            product.Tags = tags;
            product.CreatedById = _webContextManager.CurrentUserId;

            if (model.Specifications != null)
            {
                var productSpecifications = new List<ProductSpecification>();
                foreach (var specification in model.Specifications)
                {
                    if (specification.SpecificationOptionIdList != null)
                    {
                        foreach (var specificationOption in specification.SpecificationOptionIdList)
                        {
                            var productSpecification = new ProductSpecification
                            {
                                SpecificationId = specification.Id,
                                SpecificationOptionId = specificationOption
                            };
                            productSpecifications.Add(productSpecification);
                        }
                    }
                    else if (specification.Value != null)
                    {
                        var productSpecification = new ProductSpecification
                        {
                            SpecificationId = specification.Id,
                            Value = specification.Value
                        };
                        productSpecifications.Add(productSpecification);
                    }
                }
                product.Specifications = productSpecifications;
            }

            _productRepository.Add(product);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityInserted(product);
        }

        public async Task CreateBulkByViewModelAsync(ProductBulkCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            foreach (var productBulk in model.ProductBulks)
            {
                var request = new ProductSearchModel
                {
                    ColorType = productBulk.Color,
                    GuaranteeId = productBulk.GuaranteeId != null && productBulk.GuaranteeId.IsValidGuid() ? productBulk.GuaranteeId.ToNullableGuid(): null,
                    CatalogId = productBulk.CatalogId,
                    IsSecondHand = productBulk.IsSecondHand,
                    SecondHandCode = productBulk.SecondHandCode

                };
                var anyCatalog = await AnyByRequestAsync(request);
                if (anyCatalog)
                    continue;

                var product = _mapper.Map<Product>(productBulk);
                product.IsCatalog = true;
                product.Code = await _catalogRepository.AsNoTracking()
                    .Where(m => m.Id == productBulk.CatalogId)
                    .Select(m => m.Code).SingleOrDefaultAsync();
                product.State = StateType.Approved;
                product.CreatedOn = DateTime.Now;
                product.ModifiedOn = DateTime.Now;
                //product.CompanyId = _companyRepository.CurrentCompanyId;
                product.Sell = productBulk.AvailableCount > 0 ? SellType.Available : SellType.Unavailable;
                product.CreatedById = _webContextManager.CurrentUserId;
                product.CreatedOn = DateTime.Now;
                product.IsSecondHand = productBulk.IsSecondHand;
                if (productBulk.IsSecondHand == true)
                {
                    product.SecondHandCode = productBulk.SecondHandCode;
                    product.Description = productBulk.Description;
                }
                if (productBulk.GuaranteeId != null && productBulk.GuaranteeId.IsValidGuid())
                    product.GuaranteeId = productBulk.GuaranteeId.ToNullableGuid();
                else
                    product.GuaranteeTitle = productBulk.GuaranteeId;

                _productRepository.Add(product);

                await _unitOfWork.SaveAllChangesAsync();

                _eventPublisher.EntityInserted(product);
            }
        }

        public async Task<bool> AnyByRequestAsync(ProductSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).AnyAsync();
        }

        public async Task EditBulkByViewModelAsync(ProductBulkEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            foreach (var productBulk in model.ProductBulks)
            {
                if (productBulk.Id == null)
                {
                    var request = new ProductSearchModel
                    {
                        ColorType = productBulk.Color,
                        GuaranteeId = productBulk.GuaranteeId != null && productBulk.GuaranteeId.IsValidGuid() ? productBulk.GuaranteeId.ToNullableGuid() : null,
                        CatalogId = productBulk.CatalogId,
                        IsSecondHand = productBulk.IsSecondHand,
                        SecondHandCode = productBulk.SecondHandCode
                    };
                    var anyCatalog = await AnyByRequestAsync(request);
                    if (anyCatalog)
                        continue;

                    var product = _mapper.Map<Product>(productBulk);
                    product.IsCatalog = true;
                    product.CategoryId = productBulk.CategoryId;
                    product.CatalogId = productBulk.CatalogId;
                    product.Code = await _catalogRepository.AsNoTracking()
                        .Where(m => m.Id == productBulk.CatalogId)
                        .Select(m => m.Code).SingleOrDefaultAsync();
                    product.State = StateType.Approved;
                    product.ModifiedOn = DateTime.Now;
                    //product.CompanyId = _companyRepository.CurrentCompanyId;
                    product.Sell = productBulk.AvailableCount > 0 ? SellType.Available : SellType.Unavailable;
                    product.CreatedById = _webContextManager.CurrentUserId;
                    product.IsSecondHand = productBulk.IsSecondHand;
                    if (productBulk.IsSecondHand == true)
                    {
                        product.SecondHandCode = productBulk.SecondHandCode;
                        product.Description = productBulk.Description;
                    }
                    if (productBulk.GuaranteeId != null && productBulk.GuaranteeId.IsValidGuid())
                        product.GuaranteeId = productBulk.GuaranteeId.ToNullableGuid();
                    else
                        product.GuaranteeTitle = productBulk.GuaranteeId;

                    _productRepository.Add(product);

                    await _unitOfWork.SaveAllChangesAsync();

                    //_eventPublisher.EntityUpdated(product);
                }
                else
                {
                    var originalProuct = await FindByIdAsync(productBulk.Id.Value);
                    originalProuct.Color = productBulk.Color;
                    originalProuct.Price = productBulk.Price;
                    originalProuct.AvailableCount = productBulk.AvailableCount;
                    originalProuct.Sell = productBulk.AvailableCount > 0 ? SellType.Available : SellType.Unavailable;
                    originalProuct.IsSecondHand = productBulk.IsSecondHand;
                    originalProuct.ModifiedOn = DateTime.Now;
                    if (productBulk.IsSecondHand == true)
                    {
                        originalProuct.SecondHandCode = productBulk.SecondHandCode;
                        originalProuct.Description = productBulk.Description;
                    }
                    if (productBulk.GuaranteeId != null && productBulk.GuaranteeId.IsValidGuid())
                        originalProuct.GuaranteeId = productBulk.GuaranteeId.ToNullableGuid();
                    else
                        originalProuct.GuaranteeTitle = productBulk.GuaranteeId;

                    await _unitOfWork.SaveAllChangesAsync();

                    //_eventPublisher.EntityUpdated(originalProuct);
                }
            }
        }

        public async Task EditCatalogByViewModelAsync(ProductEditCatalogModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var originalProuct = await FindByIdAsync(model.Id.Value);
            originalProuct.GuaranteeId = model.GuaranteeId;
            originalProuct.Color = model.Color;
            originalProuct.Price = model.Price;
            originalProuct.AvailableCount = model.AvailableCount;
            originalProuct.Sell = model.AvailableCount > 0 ? SellType.Available : SellType.Unavailable;
            originalProuct.ModifiedOn = DateTime.Now;
            originalProuct.IsSecondHand = model.IsSecondHand;
            if (model.IsSecondHand == true)
            {
                originalProuct.SecondHandCode = model.SecondHandCode;
                originalProuct.Description = model.Description;
            }
            await _unitOfWork.SaveAllChangesAsync();

            //_eventPublisher.EntityUpdated(originalProuct);
        }

        public async Task DeleteByCodeAsync(string productCode, bool isCurrentUser = false)
        {
            var product = await _productRepository.FirstOrDefaultAsync(model => model.Code == productCode);
            if (isCurrentUser &&  product.CreatedById != _webContextManager.CurrentUserId)
                return;

            await _productImageService.RemoveRangeAsync(product.Images.ToList());
            await _productFeatureService.RemoveRangeAsync(product.Features.ToList());
            await _productTagService.RemoveRangeAsync(product.Tags.ToList());
            await _productSpecificationService.RemoveRangeAsync(product.Specifications.ToList());
            await _productVisitService.RemoveRangeAsync(product.Visits.ToList());
            await _productReviewService.RemoveRangeAsync(product.Reviews.ToList());
            await _productCommentService.RemoveRangeAsync(product.Comments.ToList());
            await _bagService.RemoveRangeAsync(product.Carts.ToList());
            await _productLikeService.RemoveRangeAsync(product.Likes.ToList());
            _productRepository.Remove(product);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(product);
        }

        public async Task<IList<ProductModel>> GetByIdsAsync(IEnumerable<Guid?> listId)
        {
            List<Product> products = new List<Product>();
            foreach (var catalogId in listId)
            {
                var ss = await _productRepository.Where(model => model.CatalogId == catalogId).ToListAsync();
                if (ss != null)
                    foreach (var item in ss)
                    {
                        products.Add(item);
                    }
            }
            return _mapper.Map<IList<ProductModel>>(products);
        }

        public async Task DeleteByUserIdAsync(Guid userId)
        {
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));

            var products = await GetProductsByUserIdAsync(userId);

            if (products.Count > 0)
                foreach (var product in products)
                    await DeleteByCodeAsync(product.Code);
        }

        public async Task EditApproveByViewModelAsync(ProductEditModel model)
        {
            var product = await _productRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            var oldProduct = (Product)product.Clone();

            await EditAsync(model, product);
            product.ApprovedById = _webContextManager.CurrentUserId;
            product.State = StateType.Approved;

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(oldProduct);
        }

        public async Task EditAsync(ProductEditModel model, Product originalProduct)
        {
            // Product
            _mapper.Map(model, originalProduct);
            originalProduct.Description = model.Description.ToSafeHtml();

            // Product Images
            var images = _mapper.Map<List<ProductImage>>(model.Images);
            if (images == null)
            {
                _productImageRepository.Where(m => m.ProductId == model.Id).Delete();
            }
            else
            {
                _productImageRepository.Where(m => m.ProductId == model.Id).Delete();
                originalProduct.Images.AddRange(images.ToArray());
            }

            //  Product Keywords
            var keywords = model.ProductKeywords;
            if (keywords == null)
            {
                _productKeywordRepository.Where(m => m.ProductId == model.Id).Delete();
            }
            else
            {
                _productKeywordRepository.Where(m => m.ProductId == model.Id).Delete();
                var productKeywords = new List<ProductKeyword>();
                foreach (var keyword in keywords)
                {
                    var productKeyword = new ProductKeyword();
                    Guid isGuid;
                    Guid.TryParse(keyword, out isGuid);
                    if (isGuid != Guid.Empty)
                        productKeyword.KeywordId = keyword.ToGuid();
                    else
                        productKeyword.Title = keyword;

                    productKeywords.Add(productKeyword);
                }
                //originalProduct.ProductKeywords.Clear();
                //.ProductKeywords = new List<ProductKeyword>();
                originalProduct.ProductKeywords.AddRange(productKeywords.ToArray());
            }

            // Product Features
            var features = _mapper.Map<List<ProductFeature>>(model.Features);
            if (features == null)
            {
                _productFeatureRepository.Where(m => m.ProductId == model.Id).Delete();
            }
            else
            {
                _productFeatureRepository.Where(m => m.ProductId == model.Id).Delete();
                originalProduct.Features.AddRange(features.Where(feature => !string.IsNullOrEmpty(feature.Title?.Trim())).ToArray());
            }

            // Product Tags
            var productTags = _mapper.Map<List<ProductTag>>(model.ProductTags);
            if (productTags == null)
            {
                _productTagRepository.Where(m => m.ProductId == model.Id).Delete();
            }
            else
            {
                _productTagRepository.Where(m => m.ProductId == model.Id).Delete();
                originalProduct.Tags.AddRange(productTags.ToArray());
            }

            // Product Specifications
            if (model.Specifications == null)
            {
                _productSpecificationRepository.Where(m => m.ProductId == model.Id).Delete();
            }
            else
            {
                _productSpecificationRepository.Where(m => m.ProductId == model.Id).Delete();
                var productSpecifications = new List<ProductSpecification>();
                foreach (var specification in model.Specifications)
                {
                    if (specification.SpecificationOptionIdList != null)
                    {
                        foreach (var specificationOption in specification.SpecificationOptionIdList)
                        {
                            var productSpecification = new ProductSpecification
                            {
                                SpecificationId = specification.Id,
                                SpecificationOptionId = specificationOption
                            };
                            productSpecifications.Add(productSpecification);
                        }
                    }
                    else if (specification.Value != null)
                    {
                        var productSpecification = new ProductSpecification
                        {
                            SpecificationId = specification.Id,
                            Value = specification.Value
                        };
                        productSpecifications.Add(productSpecification);
                    }
                }
                originalProduct.Specifications.AddRange(productSpecifications.ToArray());
            }
        }

        public async Task EditByViewModelAsync(ProductEditModel model, bool isCurrentUser = false)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var originalProduct = await _productRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            if(isCurrentUser && originalProduct.CreatedById != _webContextManager.CurrentUserId)
                return;

            originalProduct.ModifiedOn = DateTime.Now;
            await EditAsync(model, originalProduct);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(originalProduct);
        }

        public async Task EditRejectByViewModelAsync(ProductEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var product = await _productRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            await EditAsync(model, product);
            product.ApprovedById = _webContextManager.CurrentUserId;
            product.State = StateType.Rejected;

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(product);
        }

        public async Task<Product> FindByIdAsync(Guid productId)
        {
            return await _productRepository.FirstOrDefaultAsync(model => model.Id == productId);
        }

        public async Task<Product> FindByCodeAsync(string productCode)
        {
            return await _productRepository.FirstOrDefaultAsync(model => model.Code == productCode);
        }

        public async Task<IList<Product>> GetByCodeWithCurrentUser(string productCode)
        {
            return await _productRepository.AsNoTracking()
                .Where(model => model.Code == productCode && model.CreatedById == _webContextManager.CurrentUserId)
                .ToListAsync();
        }

        public async Task<Product> FindByUserIdWithCodeAsync(Guid userId, string code)
        {
            return await _productRepository.FirstOrDefaultAsync(model => model.CreatedById == userId && model.Code == code);
        }

        public async Task<string> GenerateCodeAsync()
        {
            var lastProductCode = await _productRepository.AsNoTracking()
                .Select(model => model.Code)
                .ToListAsync();

            var lastCatalogCode = await _catalogRepository.AsNoTracking()
                .Select(model => model.Code)
                .ToListAsync();

            return (lastProductCode.Concat(lastCatalogCode).Max(Convert.ToInt32).ToInt32() + 1).ToString() ?? "1";
        }

        public async Task<Location> GetAddressByIdAsync(Guid productId)
        {
            return await _productRepository
                  .AsNoTracking()
                  .Include(model => model.Location)
                  .Select(model => model.Location)
                  .FirstOrDefaultAsync(model => model.Id == productId);
        }

        public async Task<IList<SelectList>> GetAllCurrentUserAsSelectListItem()
        {
            return await _productRepository.AsNoTracking()
                .Where(model => model.State == StateType.Approved && model.CreatedById == _webContextManager.CurrentUserId)
                .Select(record => new SelectList
                {
                    Value = record.Id.ToString(),
                    Text = record.Title
                }).ToListAsync();
        }

        public async Task<IList<ProductModel>> GetApprovedByCompanyIdAsync(Guid companyId)
        {
            var request = new ProductSearchModel
            {
                PageSize = PageSize.All,
                CompanyId = companyId,
                State = StateType.Approved
            };
            var products = await GetByRequestAsync(request);
            var productsViewModel = _mapper.Map<IList<ProductModel>>(products);
            for (var i = 0; i < productsViewModel.Count; i++)
            {
                productsViewModel[i].IsExist = await _bagService.IsExistByProductIdAsync(productsViewModel[i].Id, _webContextManager.CurrentUserId);
            }
            return productsViewModel;
        }

        public Product GetByIdAsync(Guid productId)
        {
            return _productRepository
                 .AsNoTracking()
                 .FirstOrDefault(model => model.Id == productId);
        }

        public async Task<IList<Product>> GetByRequestAsync(ProductSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public async Task<IList<FineUploaderObject>> GetFileAsFineUploaderModelByIdAsync(Guid productId)
        {
            return (await _productRepository.AsNoTracking()
                    .Where(model => model.Id == productId)
                    .Select(model => new
                    {
                        model.Id,
                        model.ImageFileName
                    }).ToListAsync())
                .Select(model => new FineUploaderObject
                {
                    id = model.Id.ToString(),
                    uuid = model.Id.ToString(),
                    name = model.ImageFileName,
                    thumbnailUrl = FileConst.ImagesWebPath.PlusWord(model.ImageFileName),
                    size = new FileInfo(HttpContext.Current.Server.MapPath(FileConst.ImagesWebPath.PlusWord(model.ImageFileName))).Length.ToString()
                }).ToList();
        }

        public async Task<IList<SelectList>> GetAsSelectListAsync()
        {
            return await _productRepository.AsNoTracking()
                .Where(model => model.State == StateType.Approved)
                .Select(model => new SelectList
                {
                    Value = model.Id.ToString(),
                    Text = model.Title
                }).ToListAsync();
        }

        public async Task<IList<FineUploaderObject>> GetFilesAsFineUploaderModelByIdAsync(Guid productId)
        {
            return (await _productRepository.AsNoTracking()
                .Include(model => model.Images)
                    .Where(model => model.Id == productId)
                    .Select(model => model.Images)
                    .SingleOrDefaultAsync())
                .Select(model => new FineUploaderObject
                {
                    id = model.Id.ToString(),
                    uuid = model.Id.ToString(),
                    name = model.FileName,
                    thumbnailUrl = FileConst.ImagesWebPath.PlusWord(model.FileName),
                    size = new FileInfo(HttpContext.Current.Server.MapPath(FileConst.ImagesWebPath.PlusWord(model.FileName))).Length.ToString()
                }).ToList();
        }

        public async Task<ProductLikeListModel> GetMyListProductLikeAsync()
        {
            var listProductId = await _productRepository
                .AsNoTracking()
                .Where(model => model.CreatedById == _webContextManager.CurrentUserId)
                .Select(model => model.Id).ToListAsync();

            var viewModel = await _productLikeService.GetByProductsAsync(listProductId);
            return new ProductLikeListModel
            {
                ProductLikes = viewModel
            };
        }

        public async Task<IList<Product>> GetProductsByUserIdAsync(Guid userId)
        {
            return await _productRepository
                .AsNoTracking()
                .Where(model => model.CreatedById == userId).ToListAsync();
        }

        public async Task<bool> IsMineByCodeAsync(string productCode)
        {
            if (HttpContext.Current.User.IsInRole("CanProductEdit"))
                return true; 
            return await _productRepository.AsNoTracking().AnyAsync(model => model.Code == productCode && model.CreatedById == _webContextManager.CurrentUserId);
        }

        public async Task<decimal?> MaxByRequestAsync(ProductSearchModel model, Expression<Func<Product, decimal?>> agg)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).MaxAsync(agg);
        }

        public async Task<string> MaxCodeByRequestAsync(ProductSearchModel model , Expression<Func<Product, string>> code)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

           return await QueryByRequest(model).MaxAsync(code);
        }

        public async Task<decimal?> MinByRequestAsync(ProductSearchModel model, Expression<Func<Product, decimal?>> agg)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

           return await QueryByRequest(model).MinAsync(agg);
        }

        public IQueryable<Product> QueryByRequest(ProductSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var products = _productRepository.AsNoTracking().AsQueryable().AsExpandable()
                .Include(m => m.CreatedBy)
                .Include(m => m.CreatedBy.Meta)
                .Include(m => m.Company)
                .Include(m => m.Images)
                .Include(m => m.Category)
                .Include(m => m.Location);

            if (model.MaxPrice.HasValue)
                products = products.Where(m => m.Price <= model.MaxPrice);
            if (model.MinPrice.HasValue)
                products = products.Where(m => m.Price >= model.MinPrice);
           
            //if (request.CategoryAlias != null && request.CategoryAlias != "Category-All")
            //    products = products.Where(model => model.Category.Alias == request.CategoryAlias);
            //if (request.CategoryId != null && request.CategoryId != Guid.Parse("666c3824-0105-9e5b-b86b-0226a45db0d2"))
            //    products = products.Where(model => model.CategoryId == request.CategoryId);
           if (model.CatalogId != null)
                products = products.Where(m => m.CatalogId == model.CatalogId);
            if (model.Code != null)
                products = products.Where(m => m.Code == model.Code);
            if (model.CompanyId.HasValue)
                products = products.Where(product => product.CompanyId == model.CompanyId);
            if (model.UserId.HasValue)
                products = products.Where(product => product.CreatedById == model.UserId);
            if (model.StateId.HasValue)
                products = products.Where(m => m.Company.Location.CityId == model.StateId);
            if (model.ColorType.HasValue)
                products = products.Where(product => product.Color == model.ColorType);
            if (model.GuaranteeId.HasValue)
                products = products.Where(product => product.GuaranteeId == model.GuaranteeId);
            if (model.CategoryAlias != null && model.CategoryWithMany != true)
                products = products.Where(product => product.Category.Alias == model.CategoryAlias);
           

            //if (request.CategoryId != null && request.CategoryWithMany == true)
            //{
            //    var categories = _categoryRepository.Where(model => model.Id == request.CategoryId)
            //        .SelectMany(model => model.Childrens).Select(model => model.Id).ToList();
            //    products = products.Where(product => categories.Contains(product.CategoryId.Value) || product.Category.Id == request.CategoryId);
            //}
            if (model.Ids != null && model.Ids.Any())
                products = products.Where(m => model.Ids.Contains(m.Id));
            if (model.CreatedById.HasValue)
                products = products.Where(m => m.CreatedById == model.CreatedById);
            if (model.Term.HasValue())
                products = products.Where(product => product.Title.Contains(model.Term) || product.Description.Contains(model.Term) || product.Catalog.Title.Contains(model.Term));
            if (model.State.HasValue)
                if (model.State != StateType.All)
                    products = products.Where(product => product.State == model.State);
            if (model.Sell.HasValue)
                products = products.Where(product => product.Sell == model.Sell);
            if (model.AvailableCountGreater.HasValue)
                products = products.Where(product => product.AvailableCount > model.AvailableCountGreater);

            if (model.SpecificationsDictionary != null && model.SpecificationsDictionary.Any())
            {
                var predicate = PredicateBuilder.False<Product>();
                foreach (var dictionary in model.SpecificationsDictionary)
                {
                    foreach (var value in dictionary.Value)
                    {
                        var specId = _specificationService.GetIdByTitle(dictionary.Key, model.CategoryId.Value);
                        if (specId != null)
                        {
                            var specOptionId = _specificationOptionService.GetIdByTitle(value, specId.Value);
                            predicate = predicate.Or(n => n.Specifications.Any(m => m.SpecificationOptionId == specOptionId && m.SpecificationId == specId) ||
                                n.Catalog.Specifications.Any(m => m.SpecificationOptionId == specOptionId && m.SpecificationId == specId));
                            products = products.Where(predicate);
                        }
                    }
                }
            }
            if (model.CategoryAlias != null && model.CategoryWithMany == true)
            {
                var predicate = PredicateBuilder.False<Product>();
                if (model.CategoryAlias.HasValue())
                    predicate = predicate.Or(product => product.Category.Alias == model.CategoryAlias);
                var categoryByAlias = _categoryRepository.AsNoTracking().SingleOrDefault(category => category.Alias == model.CategoryAlias);
                var categories = _categoryRepository.AsNoTracking().ToList().GetAllChildsById(categoryByAlias);
                foreach (var category in categories)
                {
                    Guid? categoryId = category.Id;
                    predicate = predicate.Or(product => product.CategoryId == categoryId);
                }
              products = products.Where(predicate);
            }
            if (model.Colors != null && model.Colors.Any())
            {
                var predicate = PredicateBuilder.False<Product>();
                foreach (var color in model.Colors)
                {
                    var enumValue = EnumHelper.GetEnumByValue<ColorType>(color.ToInt32());
                    predicate = predicate.Or(m => m.Color == enumValue);
                }
                products = products.Where(predicate);
            }

            //if (request.GroupBy != null)
            //    products = products.GroupBy(request.GroupBy).Select(grouping => grouping.FirstOrDefault());

            products = products.OrderBy($"{model.SortMember ?? SortMember.CreatedOn} {model.SortDirection ?? SortDirection.Desc}");
            //products = request.SortDirection == SortDirection.Asc ?
            //    products.AddOrAppendOrderBy(request.SortMember) :
            //    products.AddOrAppendOrderByDescending(request.SortMember);

            return products;
        }

        public async Task<IList<SelectList>> CastQueryDictionaryToRequestValues(Dictionary<string, List<string>> queryDictionary)
        {
            var titleList = await _specificationService.GetTitlesAsync();
            if (queryDictionary != null)
            {
                var requestValues = new List<SelectList>();
                foreach (var keyValuePair in queryDictionary)
                {
                    switch (keyValuePair.Key)
                    {
                        case "price":

                            requestValues.Add(new SelectList
                            {
                                Value = keyValuePair.Key,
                                Text = keyValuePair.Value[0],
                                Description = $"قیمت از {keyValuePair.Value[0].Split("-")[0].CastToRegularCurrency()} تا {keyValuePair.Value[0].Split("-")[1].CastToRegularCurrency()}"
                            });

                            break;

                        case "color":
                            foreach (var color in keyValuePair.Value)
                            {
                                var colorName = EnumHelper.GetDescription<ColorType>(color.ToInt32());
                                requestValues.Add(new SelectList
                                {
                                    Value = keyValuePair.Key,
                                    Text = keyValuePair.Value[0],
                                    Description = $" رنگ  :{colorName}"
                                });
                            }

                            break;

                        case "StateId":
                            var cityName = await _cityService.GetNameByIdAsync(keyValuePair.Value[0].ToGuid());
                            requestValues.Add(new SelectList
                            {
                                Value = keyValuePair.Key,
                                Text = keyValuePair.Value[0],
                                Description = $": شهر {cityName}"
                            });
                            break;

                        default:
                            if (titleList.Contains(keyValuePair.Key))
                            {
                                foreach (var valuePair in keyValuePair.Value)
                                {
                                    requestValues.Add(new SelectList
                                    {
                                        Value = keyValuePair.Key,
                                        Text = valuePair,
                                        Description = $"{keyValuePair.Key} : {valuePair}"
                                    });
                                }
                            }
                            break;
                    }
                }
                return requestValues;
            }
            return null;
        }

        public async Task<IList<ProductModel>> GetByCatalogIdAsync(Guid catalogId)
        {
            var products = await _productRepository.AsNoTracking().Where(model => model.CatalogId == catalogId).ToListAsync();
            
            return _mapper.Map<List<ProductModel>>(products);
        }

        public async Task<decimal?> SumByRequestAsync(ProductSearchModel model, string aggregateMember)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var products = QueryByRequest(model);
            switch (aggregateMember)
            {
                case "Price":
                    var memberSum = await products.SumAsync(m => m.Price);
                    return memberSum;
            }

            return null;
        }

        public async Task SetStateByIdAsync(Guid productId, StateType state)
        {
            var product = await FindByIdAsync(productId);
            product.State = state;

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(product);
        }

        #endregion Public Methods
    }
}