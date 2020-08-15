using Advertise.Core.Domain.Users;
using Advertise.Core.Model.Users;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Users
{
    public class UserNotificationProfile : BaseProfile
    {
        public UserNotificationProfile()
        {
            CreateMap<UserNotification, UserNotificationListModel>(MemberList.None).ReverseMap();

            CreateMap<UserNotification, UserNotificationCreateModel>(MemberList.None).ReverseMap();

            CreateMap<UserNotification, UserNotificationModel>(MemberList.None)
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Target.Code));
        }
    }
}