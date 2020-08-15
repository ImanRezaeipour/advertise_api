using Advertise.Core.Common;
using Advertise.Core.Domain.Announces;
using Advertise.Core.Model.Announces;
using Advertise.Core.Model.Common;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Announces
{
    public class AnnounceOptionProfile : BaseProfile
    {
        public AnnounceOptionProfile()
        {
            CreateMap<AnnounceOption, AnnounceOptionModel>(MemberList.None).ReverseMap();
            
            CreateMap<AnnounceOption, AnnounceOptionCreateModel>(MemberList.None).ReverseMap();
            
            CreateMap<AnnounceOption, AnnounceOptionEditModel>(MemberList.None).ReverseMap();
            
            CreateMap<AnnounceOption, AnnounceOptionSearchModel>(MemberList.None).ReverseMap();
            
            CreateMap<AnnounceOption, SelectList>(MemberList.None).ReverseMap();
            
            CreateMap<AnnounceOption, AnnounceOptionListModel>(MemberList.None).ReverseMap();
        }
    }
}