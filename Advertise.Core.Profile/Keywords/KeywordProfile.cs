using Advertise.Core.Domain.Keywords;
using Advertise.Core.Model.Keywords;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Keywords
{
    public class KeywordProfile : BaseProfile
    {
        public KeywordProfile()
        {
            CreateMap<Keyword, KeywordModel>(MemberList.None).ReverseMap();

            CreateMap<Keyword, KeywordCreateModel>(MemberList.None).ReverseMap();

            CreateMap<Keyword, KeywordEditModel>(MemberList.None).ReverseMap();
        }
    }
}