using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Advertise.Core.Domain.Newsletters;
using Advertise.Core.Model.Newsletters;

namespace Advertise.Service.Newsletters
{
    public interface INewsletterService
    {
        #region Public Methods

        Task<int> CountByRequestAsync(NewsletterSearchModel model);
        Task CreateByViewModelAsync(NewsletterCreateModel model);
        Task DeleteByIdAsync(Guid newsletterId);
        Task<IList<Newsletter>> GetByRequestAsync(NewsletterSearchModel model);
        Task<bool> IsEmailExistAsync(string email, Guid? userId = null, CancellationToken cancellationToken = default (CancellationToken));
        IQueryable<Newsletter> QueryByRequest(NewsletterSearchModel model);
        Task SubscribeByViewModelAsync(NewsletterCreateModel model);

        #endregion Public Methods
    }
}