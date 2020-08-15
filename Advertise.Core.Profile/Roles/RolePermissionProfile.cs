using Advertise.Core.Domain.Roles;
using Advertise.Core.Model.Roles;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Roles
{
    public class RolePermissionProfile : BaseProfile
    {
        public RolePermissionProfile()
        {
            CreateMap<RolePermission, RolePermissionModel>(MemberList.None).ReverseMap();
        }
    }
}