using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Exceptions;
using Advertise.Core.Model.Products;
using Advertise.Service.Common;
using Advertise.Service.Products;
using Advertise.Service.Specifications;
using AutoMapper;

namespace Advertise.Service.Factory.Products
{
    public class ProductSpecificationFactory : IProductSpecificationFactory
    {
        #region Private Fields

        private readonly ICommonService _commonService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly IProductSpecificationService _productSpecificationService;
        private readonly ISpecificationService _specificationService;

        #endregion Private Fields

        #region Public Constructors

        public ProductSpecificationFactory(ICommonService commonService, ISpecificationService specificationService, IProductSpecificationService productSpecificationService, IMapper mapper, IProductService productService, IListService listService)
        {
            _commonService = commonService;
            _specificationService = specificationService;
            _productSpecificationService = productSpecificationService;
            _mapper = mapper;
            _productService = productService;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<IList<ProductSpecificationCreateModel>> PrepareCreateModelAsync(Guid categoryId)
        {
            if (categoryId == null)
                throw new ArgumentNullException(nameof(categoryId));

            var specifications = await _specificationService.GetByCategoryIdAsync(categoryId);
            var viewModel = _mapper.Map<IList<ProductSpecificationCreateModel>>(specifications);

            return viewModel;
        }

        public async Task<ProductSpecificationDetailModel> PrepareDetailModelAsync(Guid productSpecificationId)
        {
            var productSpecification = await _productSpecificationService.FindByIdAsync(productSpecificationId);
            var viewmodel = _mapper.Map<ProductSpecificationDetailModel>(productSpecification);

            return viewmodel;
        }

        public async Task<IList<ProductSpecificationEditModel>> PrepareEditModelAsync(Guid productId)
        {
            var product = await _productService.FindByIdAsync(productId);
            if(product.CategoryId == null)
                throw new FactoryException();
            var specifications = await _specificationService.GetByCategoryIdAsync(product.CategoryId.Value);

            var viewModel = _mapper.Map<IList<ProductSpecificationEditModel>>(specifications);

            //viewModel = viewModel.Select(c =>
            //{
            //    c.SpecificationOptionIdList = new List<Guid>();
            //    return c;
            //}).ToList();

            var productSpecificationRequest = new ProductSpecificationSearchModel
            {
                PageSize = PageSize.All,
                ProductId = productId
            };
            var productSpecifications = await _productSpecificationService.GetByRequestAsync(productSpecificationRequest);

            foreach (var item in viewModel)
            {
                item.Value = productSpecifications.SingleOrDefault(model => model.SpecificationId == item.Id)?.Value;
                item.SpecificationOptionIdList = productSpecifications.Where(model => model.SpecificationId == item.Id)
                    .Select(model => model.SpecificationOptionId).ToList();
            }

            //foreach (var productSpecification in productSpecifications)
            //{
            //    var specification = viewModel.FirstOrDefault(model => model.Id == productSpecification.SpecificationId);
            //    if (specification == null)
            //        continue;

            //    var index = viewModel.IndexOf(specification);
            //    viewModel[index].Value = productSpecification.Value;
            //    if (productSpecification.SpecificationOptionId != null)
            //    {
            //        viewModel[index].SpecificationOptionIdList.Add(productSpecification.SpecificationOptionId.Value);
            //    }
            //}

            //var specifications = (from spec in _specification
            //    join prod in _productSpecificationRepository on spec.Id equals prod.SpecificationId into p
            //    from sub in p.DefaultIfEmpty()
            //    where spec.CategoryId == categoryId
            //    select new { spec.Id, spec.Description, spec.Title, spec.Type, spec.Order, SpecificationOptionId = (sub == null ? Guid.Empty : sub.SpecificationOptionId), Value = (sub == null ? "" : sub.Value) }).ToList();

            //var list = new List<ProductSpecificationNewViewModel>();

            //foreach (var specification in specifications)
            //{
            //    list.Add(new ProductSpecificationNewViewModel()
            //    {
            //        Title = specification.Title,
            //        Description = specification.Description,
            //        Order = specification.Order.GetValueOrDefault(),
            //        SpecificationOptionId = specification.SpecificationOptionId.GetValueOrDefault(),
            //        Value = specification.Value,
            //        Type = specification.Type.GetValueOrDefault(),
            //        SpecificationOptions = await _specificationOptionService.GetViewModelBySpecificationIdAsync(specification.Id)
            //    });
            //}

            return viewModel;
        }

        public async Task<ProductSpecificationListModel> PrepareListModelAsync(ProductSpecificationSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _productSpecificationService.CountByRequestAsync(model);
            var productSpecifications = await _productSpecificationService.GetByRequestAsync(model);
            var productSpecificationViewModel = _mapper.Map<IList<ProductSpecificationModel>>(productSpecifications);
            var viewModel = new ProductSpecificationListModel
            {
                SearchModel = model,
                ProductSpecifications = productSpecificationViewModel,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync(),
            };

            return viewModel;
        }

        #endregion Public Methods
    }
}