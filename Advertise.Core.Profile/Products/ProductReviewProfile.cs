using Advertise.Core.Domain.Products;
using Advertise.Core.Model.Products;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Products
{
    public class ProductReviewProfile : BaseProfile
    {
        public ProductReviewProfile()
        {
            CreateMap<ProductReview, ProductReviewCreateModel>(MemberList.None).ReverseMap();

            CreateMap<ProductReview, ProductReviewDetailModel>(MemberList.None).ReverseMap();

            CreateMap< ProductReviewEditModel, ProductReview>(MemberList.None);

            CreateMap<ProductReview, ProductReviewEditModel>(MemberList.None);

            CreateMap<ProductReview, ProductReviewListModel>(MemberList.None).ReverseMap();

            CreateMap<ProductReview, ProductReviewModel>(MemberList.None).ReverseMap();

            CreateMap<ProductReview, ProductReviewSearchModel>(MemberList.None).ReverseMap();
        }
    }
}