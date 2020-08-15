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
    public class CatalogFeatureService : ICatalogFeatureService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<CatalogFeature> _catalogFeatureRepository;
        private readonly IMapper _mapper;
        private readonly IEventPublisher _eventPublisher;

        public CatalogFeatureService(IUnitOfWork unitOfWork, IMapper mapper, IEventPublisher eventPublisher)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _eventPublisher = eventPublisher;
            _catalogFeatureRepository = unitOfWork.Set<CatalogFeature>();
        }

        public IQueryable<CatalogFeature> QueryByRequest(CatalogFeatureSearchModel request)
        {
            if(request == null)
                throw new ArgumentNullException(nameof(request));

            var catalogFeatures = _catalogFeatureRepository.AsNoTracking().AsQueryable();

            if (request.CreatedOn != null)
                catalogFeatures = catalogFeatures.Where(feature => feature.CreatedOn == request.CreatedOn);
            if (request.CatalogId != null)
                catalogFeatures = catalogFeatures.Where(feature => feature.CatalogId == request.CatalogId);

            catalogFeatures = catalogFeatures.OrderBy($"{request.SortMember ?? SortMember.CreatedOn} {request.SortDirection ?? SortDirection.Desc}");

            return catalogFeatures;
        }

        public async Task<IList<CatalogFeature>> GetByRequestAsync(CatalogFeatureSearchModel request)
        {
            if(request == null)
                throw new ArgumentNullException(nameof(request));

            return await QueryByRequest(request).ToPagedListAsync(request.PageIndex, request.PageSize);
        }
    }
}
