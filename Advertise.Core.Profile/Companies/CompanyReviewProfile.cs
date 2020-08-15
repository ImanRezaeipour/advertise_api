using Advertise.Core.Domain.Companies;
using Advertise.Core.Model.Companies;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Companies
{
    public class CompanyReviewProfile : BaseProfile
    {
        public CompanyReviewProfile()
        {
            CreateMap<CompanyReview, CompanyReviewCreateModel>(MemberList.None).ReverseMap();

            CreateMap<CompanyReview, CompanyReviewDetailModel>(MemberList.None).ReverseMap();

            CreateMap<CompanyReview, CompanyReviewListModel>(MemberList.None).ReverseMap();

            CreateMap<CompanyReview, CompanyReviewModel>(MemberList.None).ReverseMap();

            CreateMap<CompanyReview, CompanyReviewSearchModel>(MemberList.None).ReverseMap();

            CreateMap< CompanyReviewEditModel, CompanyReview>(MemberList.None)
                .ForMember(dest => dest.CompanyId, opt => opt.Ignore());

            CreateMap<CompanyReview, CompanyReviewEditModel>(MemberList.None)
                .ForMember(dest => dest.CompanyId, opt => opt.Ignore())
                .ForMember(dest => dest.CompanyTitle, opt => opt.MapFrom(src => src.Company.Title));
        }
    }
}