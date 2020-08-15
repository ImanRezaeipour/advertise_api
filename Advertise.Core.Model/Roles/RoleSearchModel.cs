using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Roles
{
    public class RoleSearchModel : BaseSearchModel
    {
        public Guid? CreatedById { get; set; }
    }
}