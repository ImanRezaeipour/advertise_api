using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Announces;

namespace Advertise.Service.Factory.Announces
{
    public interface IAnnounceFactory
    {
        Task<AnnounceCreateModel> PrepareCreateModel(AnnounceCreateModel viewModelPrepare = null);
        Task<AnnounceEditModel> PrepareEditViewModelAsync(Guid id);
    }
}