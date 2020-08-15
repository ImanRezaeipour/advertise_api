using Advertise.Core.Domain.Categories;
using Advertise.Core.Model.Categories;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Categories
{
    public class CategoryFollowProfile : BaseProfile
    {
        public CategoryFollowProfile()
        {
            CreateMap<CategoryFollow, CategoryFollowCreateModel>(MemberList.None).ReverseMap();

            CreateMap<CategoryFollow, CategoryFollowEditModel>(MemberList.None).ReverseMap();

            CreateMap<CategoryFollow, CategoryFollowModel>(MemberList.None)
                .ForMember(dest => dest.CategoryTitle, opts => opts.MapFrom(src => src.Category.Title))
                .ForMember(dest => dest.CategoryAlias, opts => opts.MapFrom(src => src.Category.Alias))
                .ForMember(dest => dest.CategoryMetaTitle, opts => opts.MapFrom(src => src.Category.MetaTitle))
                .ForMember(dest => dest.CategoryImageFileName, opts => opts.MapFrom(src => src.Category.ImageFileName))
                .ForMember(dest => dest.ParentCategoryMetaTitle, opts => opts.MapFrom(src => src.Category.Parent.MetaTitle))
                .ForMember(dest => dest.ParentCategoryTitle, opts => opts.MapFrom(src => src.Category.Parent.Title))
                .ForMember(dest => dest.ParentCategoryAlias, opts => opts.MapFrom(src => src.Category.Parent.Alias));
        }
    }
}