using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Users
{
    public class UserSettingSearchModel : BaseSearchModel
    {
        public Guid? CreatedById { get; set; }
    }
}