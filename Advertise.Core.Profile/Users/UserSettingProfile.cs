using Advertise.Core.Domain.Users;
using Advertise.Core.Model.Users;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Users
{
    public class UserSettingProfile : BaseProfile
    {
        public UserSettingProfile()
        {
            CreateMap<UserSetting, UserSettingEditModel>(MemberList.None).ReverseMap();

            CreateMap<UserSetting, UserSettingSearchModel>(MemberList.None).ReverseMap();

            CreateMap<UserSetting, UserSettingModel>(MemberList.None)
                .ForMember(dest => dest.UserLastName ,opt => opt.MapFrom(sur => sur.CreatedBy.Meta.LastName));

            CreateMap<UserSetting, UserSettingListModel>(MemberList.None).ReverseMap();
        }
    }
}