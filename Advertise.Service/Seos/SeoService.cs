using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Seos;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Seos
{
    public class SeoService : ISeoService
    {
        #region Private Fields

        private readonly IMapper _mapper;
        private readonly IDbSet<Core.Domain.Seos.Seo> _seoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public SeoService(IUnitOfWork unitOfWork, IMapper mapper, IWebContextManager webContextManager)
        {
            _unitOfWork = unitOfWork;
            _seoRepository = unitOfWork.Set<Core.Domain.Seos.Seo>();
            _mapper = mapper;
            _webContextManager = webContextManager;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task CreateByViewModelAsync(SeoCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var seo = _mapper.Map<Core.Domain.Seos.Seo>(model);
           _seoRepository.Add(seo);

            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid seoId)
        {
            if (seoId == Guid.Empty)
                throw new ArgumentNullException(nameof(seoId));

            var seo = await _seoRepository.FirstOrDefaultAsync(model => model.Id == seoId);
            _seoRepository.Remove(seo);

            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task<bool> IsExistCategoryByIdAsync(string categoryId)
        {
            return await _seoRepository.AnyAsync(model => model.IsActive == true && model.EntityName == "Category" && model.EntityId == categoryId);
        }

        #endregion Public Methods
    }
}