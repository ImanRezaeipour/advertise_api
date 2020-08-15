using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Configuration;
using Advertise.Core.Domain.Smses;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Model.Smses;
using Advertise.Data.DbContexts;
using AutoMapper;
using Microsoft.AspNet.Identity;
using SmsIrRestful;

namespace Advertise.Service.Messages
{
    public class SmsService : IIdentityMessageService, ISmsService
    {
        #region Private Fields

        private readonly IConfigurationManager _configurationManager;
        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IDbSet<Sms> _smsRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public SmsService(IMapper mapper, IUnitOfWork unitOfWork, IConfigurationManager configurationManager, IEventPublisher eventPublisher)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _configurationManager = configurationManager;
            _eventPublisher = eventPublisher;
            _smsRepository = unitOfWork.Set<Sms>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(SmsSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var smses = await QueryByRequest(model).CountAsync();

            return smses;
        }

        public async Task CreateByViewModelAsync(SmsCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var sms = _mapper.Map<Sms>(model);
            _smsRepository.Add(sms);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityInserted(sms);
        }

        public async Task DeleteByIdAsync(Guid smsId)
        {
            var sms = await _smsRepository.FirstOrDefaultAsync(model => model.Id == smsId);
            _smsRepository.Remove(sms);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(sms);
        }

        public async Task EditByViewModelAsync(SmsEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var sms = await _smsRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            _mapper.Map(model, sms);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(sms);
        }

        public async Task<Sms> FindByIdAsync(Guid smsId)
        {
            return  await _smsRepository
                .FirstOrDefaultAsync(model => model.Id == smsId);
        }

        public async Task<IList<Sms>> GetByRequestAsync(SmsSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return  await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public IQueryable<Sms> QueryByRequest(SmsSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var smses = _smsRepository.AsNoTracking().AsQueryable();
            return smses.OrderBy($"{model.SortMember} {model.SortDirection}");

        }

        public Task SendAsync(IdentityMessage message)
        {
           // if (_configurationManager.SmsEnabled.ToBoolean() == false)
           //     return Task.FromResult(0);

            var token = new Token().GetToken(_configurationManager.SmsUserApiKey, _configurationManager.SmsSecretKey);

            var messageSendObject = new MessageSendObject
            {
                Messages = new List<string>
                {
                    message.Body
                }.ToArray(),
                MobileNumbers = new List<string>
                {
                    message.Destination
                }.ToArray(),
                LineNumber = _configurationManager.SmsLineNumber,
                SendDateTime = null,
                CanContinueInCaseOfError = true
            };
            var messageSendResponseObject = new MessageSend().Send(token, messageSendObject);

            if (messageSendResponseObject.IsSuccessful)
            {
            }
            return Task.FromResult(0);
        }

        #endregion Public Methods
    }
}