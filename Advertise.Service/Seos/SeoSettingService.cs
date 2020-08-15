using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Seos;
using Advertise.Core.Managers.Event;
using Advertise.Core.Model.Seos;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Seos
{
    public class SeoSettingService : ISeoSettingService
    {
        #region Private Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IDbSet<SeoSetting> _seoSettingRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public SeoSettingService(IUnitOfWork unitOfWork, IMapper mapper, IEventPublisher eventPublisher)
        {
            _unitOfWork = unitOfWork;
            _seoSettingRepository = unitOfWork.Set<SeoSetting>();
            _mapper = mapper;
            _eventPublisher = eventPublisher;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task EditByViewModelAsync(SeoSettingEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var seoSetting = await _seoSettingRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            if (seoSetting == null)
            {
                var addSeoSetting = _mapper.Map<SeoSetting>(model);
                _seoSettingRepository.Add(addSeoSetting);

                await _unitOfWork.SaveAllChangesAsync();

                _eventPublisher.EntityInserted(addSeoSetting);
            }
            else
            {
                _mapper.Map(model, seoSetting);

                await _unitOfWork.SaveAllChangesAsync();

                _eventPublisher.EntityUpdated(seoSetting);
            }
        }

        public SeoSettingModel GetFirst()
        {
            var seoSetting = _seoSettingRepository.AsNoTracking().FirstOrDefault();
            return _mapper.Map<SeoSettingModel>(seoSetting);
        }

        public async Task<SeoSetting> GetFirstAsync()
        {
            return await _seoSettingRepository.AsNoTracking().FirstOrDefaultAsync();
        }

        #endregion Public Methods
    }
}