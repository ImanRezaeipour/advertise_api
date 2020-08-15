using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Catalogs;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Model.Catalogs;
using Advertise.Core.Model.Common;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Catalogs
{
    public class CatalogImageService : ICatalogImageService
    {
        #region Private Fields

        private readonly IDbSet<CatalogImage> _catalogImageRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public CatalogImageService(IUnitOfWork unitOfWork, IMapper mapper, IEventPublisher eventPublisher)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _eventPublisher = eventPublisher;
            _catalogImageRepository = unitOfWork.Set<CatalogImage>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<IList<CatalogImage>> GetByCatalogIdAsync(Guid catalogId)
        {
            var request = new CatalogImageSearchModel
            {
                CatalogId = catalogId
            };

            return await GetByRequestAsync(request);
        }

        public async Task<IList<CatalogImage>> GetByRequestAsync(CatalogImageSearchModel request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return await QueryByRequest(request).ToPagedListAsync(request.PageIndex, request.PageSize);
        }

        public IQueryable<CatalogImage> QueryByRequest(CatalogImageSearchModel request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var catalogImages = _catalogImageRepository.AsNoTracking().AsQueryable();

            if (request.CreatedOn != null)
                catalogImages = catalogImages.Where(image => image.CreatedOn == request.CreatedOn);
            if (request.CatalogId != null)
                catalogImages = catalogImages.Where(image => image.CatalogId == request.CatalogId);

            //if (string.IsNullOrEmpty(request.SortMember))
            //    request.SortMember = SortMember.CreatedOn;
            //if (string.IsNullOrEmpty(request.SortDirection))
            //    request.SortDirection = SortDirection.Desc;

            catalogImages = catalogImages.OrderBy($"{request.SortMember ?? SortMember.CreatedOn} {request.SortDirection ?? SortDirection.Desc}");

            return catalogImages;
        }

        #endregion Public Methods
    }
}