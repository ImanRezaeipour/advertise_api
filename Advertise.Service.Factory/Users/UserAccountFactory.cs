using System.Threading.Tasks;
using Advertise.Core.Helpers;
using Advertise.Core.Model.Users;
using Advertise.Core.Types;
using Advertise.Service.Common;
using Advertise.Service.Plans;

namespace Advertise.Service.Factory.Users
{
    public class UserAccountFactory : IUserAccountFactory
    {
        #region Private Fields

        private readonly IListService _listService;
        private readonly IPlanService _planService;

        #endregion Private Fields

        #region Public Constructors

        public UserAccountFactory(IPlanService planService, IListService listService)
        {
            _planService = planService;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<UserOperatorCreateModel> PrepareUserOperatorCreateModel(UserOperatorCreateModel modelPrepare = null)
        {
            var viewModel = modelPrepare ?? new UserOperatorCreateModel();
            viewModel.RoleList = await _planService.GetAllAsSelectListItemAsync();
            viewModel.PaymentTypeList = EnumHelper.CastToSelectListItems<PaymentType>();

            return viewModel;
        }

        #endregion Public Methods
    }
}