using Advertise.Core.Domain.Products;
using Advertise.Core.Model.Products;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Products
{
    public class ProductLikeProfile : BaseProfile
    {
        public ProductLikeProfile()
        {
            CreateMap<ProductLike, ProductLikeEditModel>(MemberList.None).ReverseMap();

            CreateMap<ProductLike, ProductLikeCreateModel>(MemberList.None).ReverseMap();

            CreateMap<ProductLike, ProductLikeModel>(MemberList.None).ReverseMap();

            CreateMap<ProductLike, ProductLikeListModel>(MemberList.None).ReverseMap();

            CreateMap<ProductLike, ProductLikeSearchModel>(MemberList.None).ReverseMap();
        }
    }
}