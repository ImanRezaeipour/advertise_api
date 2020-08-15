using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Settings;
using Advertise.Core.Managers.Event;
using Advertise.Core.Model.Settings;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Settings
{
    public class SettingService : ISettingService
    {
        #region Private Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IDbSet<Setting> _settingRepositiry;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public SettingService(IUnitOfWork unitOfWork, IMapper mapper, IEventPublisher eventPublisher)
        {
            _unitOfWork = unitOfWork;
            _settingRepositiry = unitOfWork.Set<Setting>();
            _mapper = mapper;
            _eventPublisher = eventPublisher;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task EditByViewModel(SettingEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var setting = await _settingRepositiry.FirstOrDefaultAsync(m => m.Id == model.Id);
            if (setting == null)
            {
                var addSetting = _mapper.Map<Setting>(model);
                _settingRepositiry.Add(addSetting);

                await _unitOfWork.SaveAllChangesAsync();

                _eventPublisher.EntityInserted(addSetting);
            }
            else
            {
                _mapper.Map(model, setting);

                await _unitOfWork.SaveAllChangesAsync();

                _eventPublisher.EntityUpdated(setting);
            }
        }

        public SettingModel GetFirst()
        {
            var setting = _settingRepositiry.AsNoTracking().FirstOrDefault();
            return  _mapper.Map<SettingModel>(setting);
            
        }

        public async Task<Setting> GetFirstAsync()
        {
           return  await _settingRepositiry.AsNoTracking().FirstOrDefaultAsync();
            
        }

        #endregion Public Methods
    }
}