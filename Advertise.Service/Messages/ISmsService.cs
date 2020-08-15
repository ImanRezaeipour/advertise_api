using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Smses;
using Advertise.Core.Model.Smses;
using Microsoft.AspNet.Identity;

namespace Advertise.Service.Messages
{
    public interface ISmsService 
    {
        Task  CreateByViewModelAsync(SmsCreateModel model);
        Task<Sms> FindByIdAsync(Guid smsId);
        Task  DeleteByIdAsync(Guid smsId);
        Task SendAsync(IdentityMessage message);
        Task  EditByViewModelAsync(SmsEditModel model);
        Task<IList<Sms>> GetByRequestAsync(SmsSearchModel model);
        Task<int> CountByRequestAsync(SmsSearchModel model);
        IQueryable<Sms> QueryByRequest(SmsSearchModel model);
    }
}