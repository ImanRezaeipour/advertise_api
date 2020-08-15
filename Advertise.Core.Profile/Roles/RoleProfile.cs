using Advertise.Core.Domain.Roles;
using Advertise.Core.Model.Roles;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Roles
{
    public class RoleProfile : BaseProfile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleModel>(MemberList.None).ReverseMap();

            CreateMap<Role, RoleCreateModel>(MemberList.None).ReverseMap();

            CreateMap<Role, RoleEditModel>(MemberList.None).ReverseMap();

            CreateMap<Role, RoleListModel>(MemberList.None).ReverseMap();
        }
    }
}