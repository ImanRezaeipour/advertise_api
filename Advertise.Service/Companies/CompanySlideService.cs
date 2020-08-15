using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Extensions;
using Advertise.Core.Helpers;
using Advertise.Core.Managers.Event;
using Advertise.Core.Managers.File;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Companies;
using Advertise.Core.Objects;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Companies
{
    public class CompanySlideService : ICompanySlideService
    {
        #region Private Fields

        private readonly IDbSet<CompanySlide> _companySlideRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public CompanySlideService(IUnitOfWork unitOfWork, IMapper mapper, IEventPublisher eventPublisher, IWebContextManager webContextManager)
        {
            _unitOfWork = unitOfWork;
            _companySlideRepository = unitOfWork.Set<CompanySlide>();
            _mapper = mapper;
            _eventPublisher = eventPublisher;
            _webContextManager = webContextManager;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(CompanySlideSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).CountAsync();
        }

        public async Task CreateByViewModelAsync(CompanySlideCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companySlide = _mapper.Map<CompanySlide>(model);
            //companySlide.CompanyId = _webContextManager.CurrentCompanyId;
            
            _companySlideRepository.Add(companySlide);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityInserted(companySlide);
        }

        public async Task DeleteByIdAsync(Guid companySlideId)
        {
            var companySlide = await FindByIdAsync(companySlideId);
            _companySlideRepository.Remove(companySlide);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(companySlide);
        }

        public async Task EditByViewModelAsync(CompanySlideEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var originalCompanySlide = await FindByIdAsync(model.Id.Value);
            _mapper.Map(model, originalCompanySlide);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(originalCompanySlide);
        }

        public async Task<CompanySlide> FindByIdAsync(Guid companySlideId)
        {
            return await _companySlideRepository.SingleOrDefaultAsync(slide => slide.Id == companySlideId);
        }

        public async Task<IList<CompanySlide>> GetByRequestAsync(CompanySlideSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public IQueryable<CompanySlide> QueryByRequest(CompanySlideSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companySlides = _companySlideRepository.AsNoTracking().AsQueryable();

            if (model.CreatedOn.HasValue)
                companySlides = companySlides.Where(slide => slide.CreatedOn == model.CreatedOn);
            if (model.CompanyId.HasValue)
                companySlides = companySlides.Where(slide => slide.CompanyId == model.CompanyId);

            companySlides = companySlides.OrderBy($"{model.SortMember ?? SortMember.CreatedOn} {model.SortDirection ?? SortDirection.Asc}");

            return companySlides;
        }

        public async Task<IList<FineUploaderObject>> GetFileAsFineUploaderModelByIdAsync(Guid companySlideId)
        {
            return (await _companySlideRepository.AsNoTracking()
                    .Where(model => model.Id == companySlideId)
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
                    thumbnailUrl = FileConst.ImagesWebPath.PlusWord(model.ImageFileName) ?? FileConst.NoLogo,
                    size = FileHelper.FileSize(FileConst.ImagesWebPath.PlusWord(model.ImageFileName))
                }).ToList();
        }

        #endregion Public Methods
    }
}