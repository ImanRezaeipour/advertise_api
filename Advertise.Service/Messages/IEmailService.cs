using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Emails;
using Advertise.Core.Model.Emails;
using Microsoft.AspNet.Identity;

namespace Advertise.Service.Messages
{
    public interface IEmailService 
    {
        Task  CreateByViewModelAsync(EmailCreateModel model);
        Task  DeleteByIdAsync(Guid emailId);
        Task<Email> FindByIdAsync(Guid emailId);
        Task SendAsync(IdentityMessage message);
        Task  EditByViewModelAsync(EmailEditModel model);
        Task<IList<Email>> GetByRequestAsync(EmailSearchModel model);
        Task<int> CountByRequestAsync(EmailSearchModel model);
        IQueryable<Email> QueryByRequest(EmailSearchModel model);
    }
}