using Advertise.Core.Domain.Categories;
using Advertise.Core.Model.Categories;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Categories
{
    public class CategoryReviewProfile : BaseProfile
    {
        public CategoryReviewProfile()
        {
            CreateMap<CategoryReview, CategoryReviewCreateModel>(MemberList.None).ReverseMap();

            CreateMap<CategoryReview, CategoryReviewEditModel>(MemberList.None)
                .ForMember(dest => dest.CategoryId, opt => opt.Ignore());

            CreateMap<CategoryReviewEditModel, CategoryReview>(MemberList.None);

            CreateMap<CategoryReview, CategoryReviewDetailModel>(MemberList.None).ReverseMap();

            CreateMap<CategoryReview, CategoryReviewListModel>(MemberList.None).ReverseMap();

            CreateMap<CategoryReview, CategoryReviewModel>(MemberList.None)
                .ForMember(dest => dest.CategoryTitle, opts => opts.MapFrom(src => src.Category.Title));

            CreateMap<CategoryReview, CategoryReviewSearchModel>(MemberList.None).ReverseMap();
        }
    }
}