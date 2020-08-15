using Advertise.Core.Domain.Seos;
using Advertise.Core.Model.Seos;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Seos
{
    public class SeoProfile : BaseProfile
    {
        public SeoProfile()
        {
            CreateMap<Seo, SeoCreateModel>(MemberList.None).ReverseMap();

            CreateMap<Seo, SeoModel>(MemberList.None).ReverseMap();
        }
    }
}