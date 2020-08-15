using Advertise.Core.Domain.Seos;
using Advertise.Core.Model.Seos;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Seos
{
    public class SeoSettingProfile : BaseProfile
    {
        public SeoSettingProfile()
        {
            CreateMap<SeoSetting, SeoSettingModel>(MemberList.None).ReverseMap();

            CreateMap<SeoSetting, SeoSettingEditModel>(MemberList.None).ReverseMap();
        }
    }
}