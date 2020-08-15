using Advertise.Core.Domain.Companies;
using Advertise.Core.Model.Companies;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Companies
{
    public class CompanyAttachmentProfile : BaseProfile
    {
        public CompanyAttachmentProfile()
        {
            CreateMap<CompanyAttachment, CompanyAttachmentCreateModel>(MemberList.None).ReverseMap();

            CreateMap<CompanyAttachment, CompanyAttachmentEditModel>(MemberList.None).ReverseMap();

            CreateMap<CompanyAttachment, CompanyAttachmentListModel>(MemberList.None).ReverseMap();

            CreateMap<CompanyAttachment, CompanyAttachmentDetailModel>(MemberList.None)
                .ForMember(dest => dest.CompanyTitle, opts => opts.MapFrom(src => src.Company.Title))
                .ForMember(dest => dest.CompanyAlias, opts => opts.MapFrom(src => src.Company.Alias))
                .ForMember(dest => dest.CompanyLogo, opts => opts.MapFrom(src => src.Company.LogoFileName));

            CreateMap<CompanyAttachment, CompanyAttachmentSearchModel>(MemberList.None).ReverseMap();

            CreateMap<CompanyAttachment, CompanyAttachmentModel>(MemberList.None)
                .ForMember(dest => dest.CompanyMobileNumber, opts => opts.MapFrom(src => src.Company.MobileNumber))
                .ForMember(dest => dest.CompanyTitle, opts => opts.MapFrom(src => src.Company.Title))
                .ForMember(dest => dest.CompanyAlias, opts => opts.MapFrom(src => src.Company.Alias))
                .ForMember(dest => dest.CompanyCode, opts => opts.MapFrom(src => src.Company.Code))
                .ForMember(dest => dest.CompanyLogoFileName, opts => opts.MapFrom(src => src.Company.LogoFileName))
                .ForMember(dest => dest.FullName, opts => opts.MapFrom(src => src.CreatedBy.Meta.FullName));

            CreateMap<CompanyAttachmentModel, CompanyAttachment>(MemberList.None);
        }
    }
}