using Advertise.Core.Domain.Categories;
using Advertise.Core.Model.Categories;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Categories
{
    public class CategoryProfile : BaseProfile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryCreateModel>(MemberList.None).ReverseMap();

            CreateMap<Category, CategoryListModel>(MemberList.None).ReverseMap();

            CreateMap<Category, CategoryModel>(MemberList.None)
                .ForMember(dest => dest.CategoryOption, opt => opt.MapFrom(src => src.CategoryOption));

            CreateMap<CategoryModel, Category>(MemberList.None);

            CreateMap<Category, CategoryEditModel>(MemberList.None)
                .ForMember(dest => dest.ParentId, opt => opt.Ignore());

            CreateMap<CategoryEditModel, Category>(MemberList.None);

            CreateMap<Category, CategoryDetailModel>(MemberList.None).ReverseMap();

            CreateMap<CategoryModel, CategoryCreateModel>(MemberList.None).ReverseMap();

            CreateMap<CategoryModel, CategoryEditModel>(MemberList.None).ReverseMap();
        }
    }
}