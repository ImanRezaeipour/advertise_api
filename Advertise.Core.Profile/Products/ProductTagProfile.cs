using Advertise.Core.Domain.Products;
using Advertise.Core.Model.Products;
using Advertise.Core.Model.Tags;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Products
{
    public class ProductTagProfile : BaseProfile
    {
        public ProductTagProfile()
        {
            CreateMap<ProductTag, ProductTagEditModel>(MemberList.None).ReverseMap();

            CreateMap<ProductTag, ProductTagListModel>(MemberList.None).ReverseMap();

            CreateMap<ProductTag, ProductTagCreateModel>(MemberList.None).ReverseMap();

            CreateMap<ProductTag, ProductTagModel>(MemberList.None).ReverseMap();

            CreateMap<ProductTag, TagModel>(MemberList.None).ReverseMap();
        }
    }
}