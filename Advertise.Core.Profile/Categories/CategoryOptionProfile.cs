using Advertise.Core.Domain.Categories;
using Advertise.Core.Model.Categories;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Categories
{
    public class CategoryOptionProfile : BaseProfile
    {
        public CategoryOptionProfile()
        {
            CreateMap<CategoryOption, CategoryOptionModel>(MemberList.None).ReverseMap();
        }
    }
}