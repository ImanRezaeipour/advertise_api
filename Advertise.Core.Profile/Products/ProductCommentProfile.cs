using Advertise.Core.Domain.Products;
using Advertise.Core.Model.Products;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Products
{
    public class ProductCommentProfile : BaseProfile
    {
        public ProductCommentProfile()
        {
            CreateMap<ProductComment, ProductCommentModel>(MemberList.None)
                .ForMember(dest => dest.UserUserName, opts => opts.MapFrom(src => src.CommentedBy.UserName))
                .ForMember(dest => dest.UserAvatar, opts => opts.MapFrom(src => src.CommentedBy.Meta.AvatarFileName))
                .ForMember(dest => dest.ProductTitle, opts => opts.MapFrom(src => src.Product.Title))
                .ForMember(dest => dest.ProductCode, opts => opts.MapFrom(src => src.Product.Code))
                .ForMember(dest => dest.ProductImageFileName, opts => opts.MapFrom(src => src.Product.ImageFileName))
                .ForMember(dest => dest.UserFullName, opts => opts.MapFrom(src => src.CommentedBy.Meta.FullName));

             CreateMap<ProductComment, ProductCommentCreateModel>(MemberList.None).ReverseMap();

            CreateMap<ProductCommentEditModel, ProductComment>(MemberList.None)
                .ForMember(dest => dest.ProductId,opt =>opt.Ignore());

            CreateMap<ProductComment, ProductCommentEditModel>(MemberList.None);

            CreateMap<ProductComment, ProductCommentDetailModel>(MemberList.None).ReverseMap();

            CreateMap<ProductComment, ProductCommentListModel>(MemberList.None).ReverseMap();

            CreateMap<ProductComment, ProductCommentSearchModel>(MemberList.None).ReverseMap();
        }
    }
}