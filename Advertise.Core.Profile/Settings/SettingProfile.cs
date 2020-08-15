using Advertise.Core.Domain.Settings;
using Advertise.Core.Model.Settings;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Settings
{
    public class SettingProfile : BaseProfile
    {
        public SettingProfile()
        {
            CreateMap<Setting, SettingModel>(MemberList.None).ReverseMap();

            CreateMap<Setting, SettingEditModel>(MemberList.None).ReverseMap();
        }
    }
}
