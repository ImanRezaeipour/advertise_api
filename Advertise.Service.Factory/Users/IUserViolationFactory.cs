using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Users;

namespace Advertise.Service.Factory.Users
{
    public interface IUserViolationFactory
    {
        Task<UserViolationDetailModel> PrepareDetailModelAsync(Guid userReportId);
        Task<UserViolationEditModel> PrepareEditModelAsync(Guid userReportId);
        Task<UserViolationListModel> PrepareListModelAsync(UserViolationSearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}