using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Permissions
{
    public class PermissionModel : BaseModel
    {
        public string Description { get; set; }
        public Guid? Id { get; set; }
        public string MethodName { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        public string Title { get; set; }
    }
}