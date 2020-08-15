using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Companies;

namespace Advertise.Service.Factory.Companies
{
    public interface ICompanySocialFactory
    {
        Task<CompanySocialEditModel> PrepareEditModelAsync(Guid? companySocialId = null, bool isCurrentUser = false, CompanySocialEditModel modelPrepare = null);
        Task<CompanySocialListModel> PrepareListModelAsync(CompanySocialSearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}