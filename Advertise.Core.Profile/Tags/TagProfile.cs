using Advertise.Core.Domain.Tags;
using Advertise.Core.Model.Tags;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Tags
{
    public class TagProfile : BaseProfile
    {
        public TagProfile()
        {
            CreateMap<Tag, TagEditModel>(MemberList.None).ReverseMap();

            CreateMap<Tag, TagListModel>(MemberList.None).ReverseMap();

            CreateMap<Tag, TagCreateModel>(MemberList.None).ReverseMap();

            CreateMap<Tag, TagModel>(MemberList.None).ReverseMap();
        }
    }
}