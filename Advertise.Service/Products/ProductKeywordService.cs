using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Products;
using Advertise.Core.Managers.Event;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Products
{
    public class ProductKeywordService : IProductKeywordService
    {
        #region Private Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IDbSet<ProductKeyword> _productKeywordRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public ProductKeywordService(IUnitOfWork unitOfWork, IMapper mapper, IEventPublisher eventPublisher)
        {
            _unitOfWork = unitOfWork;
            _productKeywordRepository = unitOfWork.Set<ProductKeyword>();
            _mapper = mapper;
            _eventPublisher = eventPublisher;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<List<Guid?>> GetIdsByProductIdAsync(Guid productId)
        {
           return await _productKeywordRepository.AsNoTracking().Where(model => model.ProductId == productId && model.KeywordId != null)
               .Select(model => model.KeywordId).ToListAsync();
        }

        public async Task<List<string>> GetTitlesByProductIdAsync(Guid productId)
        {
           return await _productKeywordRepository.AsNoTracking().Where(model => model.ProductId == productId && model.KeywordId == null)
                .Select(model => model.Title).ToListAsync();
        }

        #endregion Public Methods
    }
}