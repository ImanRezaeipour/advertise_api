using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Complaints;

namespace Advertise.Service.Factory.Complaints
{
    public interface IComplaintFactory
    {
        Task<ComplaintDetailModel> PrepareDetailModelAsync(Guid companyId);
        Task<ComplaintListModel> PrepareListModelAsync(ComplaintSearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}