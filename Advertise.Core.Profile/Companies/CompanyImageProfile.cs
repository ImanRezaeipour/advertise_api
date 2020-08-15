using Advertise.Core.Domain.Companies;
using Advertise.Core.Model.Companies;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Companies
{
    public class CompanyImageProfile : BaseProfile
    {
        public CompanyImageProfile()
        {
            CreateMap<CompanyImage, CompanyImageCreateModel>(MemberList.None).ReverseMap();

            CreateMap<CompanyImage, CompanyImageEditModel>(MemberList.None).ReverseMap();

            CreateMap<CompanyImage, CompanyImageListModel>(MemberList.None).ReverseMap();

            CreateMap<CompanyImage, CompanyImageDetailModel>(MemberList.None).ReverseMap();

            CreateMap<CompanyImage, CompanyImageSearchModel>(MemberList.None).ReverseMap();

            CreateMap<CompanyImage, CompanyImageModel>(MemberList.None)
                .ForMember(dest => dest.CompanyMobileNumber, opts => opts.MapFrom(src => src.Company.MobileNumber))
                .ForMember(dest => dest.CompanyTitle, opts => opts.MapFrom(src => src.Company.Title))
                .ForMember(dest => dest.CompanyAlias, opts => opts.MapFrom(src => src.Company.Alias))
                .ForMember(dest => dest.CompanyCode, opts => opts.MapFrom(src => src.Company.Code))
                .ForMember(dest => dest.CompanyLogoFileName, opts => opts.MapFrom(src => src.Company.LogoFileName))
                .ForMember(dest => dest.FullName, opts => opts.MapFrom(src => src.CreatedBy.Meta.FullName));

            CreateMap<CompanyImageModel, CompanyImage>(MemberList.None);
        }
    }
}