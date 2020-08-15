using Advertise.Core.Domain.Companies;
using Advertise.Core.Model.Companies;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Companies
{
    public class CompanyVideoProfile : BaseProfile
    {
        public CompanyVideoProfile()
        {
            CreateMap<CompanyVideo, CompanyVideoCreateModel>(MemberList.None).ReverseMap();

            CreateMap<CompanyVideo, CompanyVideoEditModel>(MemberList.None).ReverseMap();
            
            CreateMap<CompanyVideo, CompanyVideoListModel>(MemberList.None).ReverseMap();

            CreateMap<CompanyVideo, CompanyVideoDetailModel>(MemberList.None)
                .ForMember(dest => dest.CompanyLogo,opts => opts.MapFrom(src => src.Company.LogoFileName))
                .ForMember(dest => dest.CompanyTitle,opts => opts.MapFrom(src => src.Company.Title))
                .ForMember(dest => dest.CompanyAlias,opts => opts.MapFrom(src => src.Company.Alias));

            CreateMap<CompanyVideo, CompanyVideoSearchModel>(MemberList.None).ReverseMap();

            CreateMap<CompanyVideo, CompanyVideoModel>(MemberList.None)
                .ForMember(dest => dest.CompanyMobileNumber, opts => opts.MapFrom(src => src.Company.MobileNumber))
                .ForMember(dest => dest.CompanyTitle, opts => opts.MapFrom(src => src.Company.Title))
                .ForMember(dest => dest.CompanyAlias, opts => opts.MapFrom(src => src.Company.Alias))
                .ForMember(dest => dest.CompanyCode, opts => opts.MapFrom(src => src.Company.Code))
                .ForMember(dest => dest.CompanyLogoFileName, opts => opts.MapFrom(src => src.Company.LogoFileName))
                .ForMember(dest => dest.FullName, opts => opts.MapFrom(src => src.CreatedBy.Meta.FullName));

            CreateMap<CompanyVideoModel, CompanyVideo>(MemberList.None);
        }
    }
}
