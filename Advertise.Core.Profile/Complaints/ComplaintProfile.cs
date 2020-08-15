using Advertise.Core.Domain.Complaints;
using Advertise.Core.Model.Complaints;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Complaints
{
   public class ComplaintProfile : BaseProfile
    {
        public ComplaintProfile()
        {
            CreateMap<Complaint, ComplaintCreateModel>(MemberList.None).ReverseMap();

            CreateMap<Complaint, ComplaintModel>(MemberList.None)
                .ForMember(dest => dest.UserAvatar, opts => opts.MapFrom(src => src.CreatedBy.Meta.AvatarFileName))
                .ForMember(dest => dest.UserFullName, opts => opts.MapFrom(src => src.CreatedBy.Meta.FullName))
                .ForMember(dest => dest.UserUserName, opts => opts.MapFrom(src => src.CreatedBy.UserName));

            CreateMap<ComplaintModel, Complaint>(MemberList.None);

            CreateMap<Complaint, ComplaintDetailModel>(MemberList.None).ReverseMap();

            CreateMap<Complaint, ComplaintListModel>(MemberList.None).ReverseMap();
        }
    }
}
