using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Permissions
{
    public class PermissionSearchModel : BaseSearchModel
    {
        public Guid? CreatedById { get; set; }
        public bool? IsCallback { get; set; }
    }
}