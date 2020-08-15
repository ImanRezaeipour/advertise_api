using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Seos;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Seos;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Seos
{
    public class SeoUrlService : ISeoUrlService
    {
        #region Private Fields

        private readonly IMapper _mapper;
        private readonly IDbSet<SeoUrl> _seoUrlRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public SeoUrlService(IUnitOfWork unitOfWork, IMapper mapper, IWebContextManager webContextManager)
        {
            _unitOfWork = unitOfWork;
            _seoUrlRepository = unitOfWork.Set<SeoUrl>();
            _mapper = mapper;
            _webContextManager = webContextManager;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(SeoUrlSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).CountAsync();
        }

        public async Task CreateByViewModelAsync(SeoUrlCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var seoUrl = _mapper.Map<SeoUrl>(model);
               seoUrl.CreatedOn = DateTime.Now;
            seoUrl.CreatedById = _webContextManager.CurrentUserId;
            _seoUrlRepository.Add(seoUrl);

            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            var seoUrl = await _seoUrlRepository.FirstOrDefaultAsync(model => model.Id == id);
            _seoUrlRepository.Remove(seoUrl);

            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task EditByViewModelAsync(SeoUrlEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var originalSeoUrl = await _seoUrlRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            _mapper.Map(model, originalSeoUrl);

            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task<SeoUrl> FindByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            return await _seoUrlRepository.FirstOrDefaultAsync(model => model.Id == id);
        }

        public Dictionary<string, string> GetAll()
        {
            return _seoUrlRepository
                .AsNoTracking()
                .Where(model => model.IsActive == true)
                .Select(model => new { model.AbsoulateUrl, model.CurrentUrl })
                .AsEnumerable()
                .ToDictionary(model => model.AbsoulateUrl, model => model.CurrentUrl);
        }

        public async Task<IList<SeoUrl>> GetByRequestAsync(SeoUrlSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public IQueryable<SeoUrl> QueryByRequest(SeoUrlSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var seoUrls = _seoUrlRepository.AsNoTracking().AsQueryable();
            if (model.CreatedById != null)
                seoUrls = seoUrls.Where(m => m.CreatedById == model.CreatedById);
            if (model.Term != null)
                seoUrls = seoUrls.Where(m => m.AbsoulateUrl.Contains(model.Term) || m.CurrentUrl.Contains(model.Term));
            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;
            seoUrls = seoUrls.OrderBy($"{model.SortMember} {model.SortDirection}");

            return seoUrls;
        }

        #endregion Public Methods
    }
}