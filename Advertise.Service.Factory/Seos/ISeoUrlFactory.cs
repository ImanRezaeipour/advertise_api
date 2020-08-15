using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Seos;

namespace Advertise.Service.Factory.Seos
{
    public interface ISeoUrlFactory
    {
        Task<SeoUrlEditModel> PrepareEditModelAsync(Guid id, SeoUrlEditModel modelPrepare = null);
        Task<SeoUrlListModel> PrepareListModelAsync(SeoUrlSearchModel model, bool isCurrentUser = false, Guid? userId = null);
        Task<SeoUrlCreateModel> PrepareCreateModelAsync(SeoUrlCreateModel modelPrepare= null);
    }
}