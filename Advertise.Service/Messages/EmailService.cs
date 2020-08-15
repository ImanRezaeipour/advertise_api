using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Mail;
using System.ServiceModel.Description;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Emails;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Emails;
using Advertise.Data.DbContexts;
using AutoMapper;
using Microsoft.AspNet.Identity;

namespace Advertise.Service.Messages
{
    public class EmailService : IIdentityMessageService, IEmailService
    {
        #region Private Fields

        private readonly IDbSet<Email> _emailRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public EmailService(IMapper mapper, IUnitOfWork unitOfWork, IEventPublisher eventPublisher)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _eventPublisher = eventPublisher;
            _emailRepository = unitOfWork.Set<Email>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(EmailSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).CountAsync();
        }

        public async Task CreateByViewModelAsync(EmailCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var email = _mapper.Map<Email>(model);
            _emailRepository.Add(email);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityInserted(email);
        }

        public async Task DeleteByIdAsync(Guid emailId)
        {
            var email = await _emailRepository.FirstOrDefaultAsync(model => model.Id == emailId);
            _emailRepository.Remove(email);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(email);
        }

        public async Task EditByViewModelAsync(EmailEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var email = await _emailRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            _mapper.Map(model, email);

            await _unitOfWork.SaveAllChangesAsync();
            _eventPublisher.EntityUpdated(email);
        }

        public async Task<Email> FindByIdAsync(Guid emailId)
        {
            return await _emailRepository.FirstOrDefaultAsync(model => model.Id == emailId);
        }

        public async Task<IList<Email>> GetByRequestAsync(EmailSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public IQueryable<Email> QueryByRequest(EmailSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var emails = _emailRepository.AsNoTracking().AsQueryable();
            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;
            emails = emails.OrderBy($"{model.SortMember} {model.SortDirection}");

            return emails;
        }

        public Task SendAsync(IdentityMessage message)
        {
            var fromAddress = new MailAddress("irappstudiomail@gmail.com", "IR APP Studio Mail");
            var toAddress = new MailAddress(message.Destination, "");
            var fromPassword = "!r@pp#Mail";
            var subject = message.Subject;
            var body = message.Body;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var mail = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                Priority = MailPriority.High,
                IsBodyHtml = true
            })
            {
              smtp.Send(mail);
            }
            return Task.FromResult(0);
        }

        #endregion Public Methods
    }
}