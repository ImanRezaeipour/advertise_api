using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Announces;

namespace Advertise.Service.Factory.Announces
{
    public interface IAnnounceOptionFactory
    {
        Task<AnnounceOptionCreateModel> PrepareCreateModelAsync(AnnounceOptionCreateModel viewModelPrepare = null);
        Task<AnnounceOptionEditModel> PrepareEditModelAsync(Guid id);
        Task<AnnounceOptionListModel> PrepareListModelAsync(AnnounceOptionSearchModel request, bool isCurrentUser = false, Guid? userId = null);
    }
}