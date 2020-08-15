using Advertise.Core.Domain.Companies;
using Advertise.Core.Model.Companies;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Companies
{
    public class CompanyQuestionProfile : BaseProfile
    {
        public CompanyQuestionProfile()
        {
            CreateMap<CompanyQuestion, CompanyQuestionCreateModel>(MemberList.None).ReverseMap();

            CreateMap<CompanyQuestionEditModel, CompanyQuestion>(MemberList.None)
                .ForMember(src => src.CompanyId, opt => opt.Ignore());
            
            CreateMap<CompanyQuestion, CompanyQuestionEditModel>(MemberList.None)
                .ForMember(dest => dest.CreatorFullName, opts => opts.MapFrom(src => src.CreatedBy.Meta.FirstName + " " + src.CreatedBy.Meta.LastName))
                .ForMember(dest => dest.EditorFullName, opts => opts.MapFrom(src => src.ModifiedBy.Meta.FirstName + " " + src.ModifiedBy.Meta.LastName))
                .ForMember(dest => dest.CompanyId, opt => opt.Ignore());

            CreateMap<CompanyQuestion, CompanyQuestionDetailModel>(MemberList.None).ReverseMap();

            CreateMap<CompanyQuestion, CompanyQuestionModel>(MemberList.None)
                .ForMember(dest => dest.UserFullName,
                    opts => opts.MapFrom(src => src.CreatedBy.Meta.FirstName + " " + src.CreatedBy.Meta.LastName))
                .ForMember(dest => dest.UserAvatar, opts => opts.MapFrom(src => src.CreatedBy.Meta.AvatarFileName))
                .ForMember(dest => dest.CompanyAlias, opts => opts.MapFrom(src => src.CreatedBy.Company.Alias))
                .ForMember(dest => dest.CompanyTitle, opts => opts.MapFrom(src => src.CreatedBy.Company.Title));

            CreateMap<CompanyQuestion, CompanyQuestionListModel>(MemberList.None).ReverseMap();
        }
    }
}