using Advertise.Core.Domain.Seos;
using Advertise.Core.Model.Seos;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Seos
{
    public class SeoUrlProfile : BaseProfile
    {
        public SeoUrlProfile()
        {
            CreateMap<SeoUrl, SeoUrlCreateModel>(MemberList.None).ReverseMap();

            CreateMap<SeoUrl, SeoUrlEditModel>(MemberList.None).ReverseMap();

            CreateMap<SeoUrl, SeoUrlModel>(MemberList.None).ReverseMap();
        }
    }
}