using Advertise.Core.Domain.Permissions;
using Advertise.Core.Model.Permissions;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Permissions
{
    public class PermissionProfile : BaseProfile
    {
        public PermissionProfile()
        {
            CreateMap<Permission, PermissionModel>(MemberList.None).ReverseMap();

            CreateMap<Permission, PermissionCreateModel>(MemberList.None).ReverseMap();

            CreateMap<Permission, PermissionEditModel>(MemberList.None).ReverseMap();
        }
    }
}