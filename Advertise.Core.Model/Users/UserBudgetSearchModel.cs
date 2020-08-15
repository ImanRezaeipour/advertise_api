using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Users
{
    public class UserBudgetSearchModel : BaseSearchModel
    {
        public Guid? CreatedById { get; set; }
        public Guid? ProductId { get; set; }
    }
}