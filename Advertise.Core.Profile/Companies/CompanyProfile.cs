using Advertise.Core.Common;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Domain.Locations;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Companies;
using Advertise.Core.Model.General;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Companies
{
    public class CompanyProfile : BaseProfile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyCreateModel>(MemberList.None).ReverseMap();

            CreateMap<Company, CompanyListModel>(MemberList.None).ReverseMap();

            CreateMap<Company, CompanyRegisterModel>(MemberList.None).ReverseMap();

            CreateMap<CompanyEditModel, Company>(MemberList.None)
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedById, opt => opt.Ignore())
                .ForMember(dest => dest.Location, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore());

            CreateMap<Company, CompanyEditModel>(MemberList.None)
                .ForMember(dest => dest.CategoryTitle, opts => opts.MapFrom(src => src.Category.Title))
                .ForMember(dest => dest.CategoryAlias, opts => opts.MapFrom(src => src.Category.Alias));

            CreateMap< CompanyDetailModel, Company>(MemberList.None);
            
            CreateMap<Company, CompanyDetailModel>(MemberList.None)
                .ForMember(dest => dest.CategoryOptionProducts, opts => opts.MapFrom(src => src.Category.CategoryOption.Products))
                .ForMember(dest => dest.CategoryOptionCompanies, opts => opts.MapFrom(src => src.Category.CategoryOption.Products));

            CreateMap<Company, CompanyDetailInfoModel>(MemberList.None).ReverseMap();

            CreateMap<Company, CompanyInfoModel>(MemberList.None).ReverseMap();

            CreateMap<Company, CompanyAboutModel>(MemberList.None).ReverseMap();

            CreateMap<CompanyModel, Company>(MemberList.None);

            CreateMap<Company, CompanyModel>(MemberList.None)
                .ForMember(dest => dest.CategoryTitle, opts => opts.MapFrom(src => src.Category.Title))
                .ForMember(dest => dest.CategoryAlias, opts => opts.MapFrom(src => src.Category.Alias))
                .ForMember(dest => dest.CategoryMetaTitle, opts => opts.MapFrom(src => src.Category.MetaTitle))
                .ForMember(dest => dest.UserAvatar, opts => opts.MapFrom(src => src.CreatedBy.Meta.AvatarFileName))
                .ForMember(dest => dest.UserDisplayName, opts => opts.MapFrom(src => src.CreatedBy.Meta.DisplayName))
                .ForMember(dest => dest.UserFullName, opts => opts.MapFrom(src => src.CreatedBy.Meta.FullName))
                .ForMember(dest => dest.CategoryId, opts => opts.MapFrom(src => src.Category.Id))
                .ForMember(dest => dest.CategoryOption, opts => opts.MapFrom(src => src.Category.CategoryOption));

            CreateMap<Location, CompanyEditModel>(MemberList.None).
                ForMember(dest => dest.Location, opts => opts.Ignore());

            CreateMap<Company, ProfileMenuModel>(MemberList.None)
                .ForMember(dest => dest.CategoryOption, opt => opt.MapFrom(src => src.Category.CategoryOption));

            CreateMap<Company, ProfileMenuModel>(MemberList.None);

            CreateMap<Company, SelectList>(MemberList.None)
                .ForMember(dest => dest.Text, opts => opts.MapFrom(src => src.Title))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.Id));
        }
    }
}