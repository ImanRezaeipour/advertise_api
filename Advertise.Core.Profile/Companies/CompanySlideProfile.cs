using Advertise.Core.Domain.Companies;
using Advertise.Core.Model.Companies;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Companies
{
    public class CompanySlideProfile : BaseProfile
    {
        public CompanySlideProfile()
        {
            CreateMap<CompanySlide, CompanySlideModel>(MemberList.None).ReverseMap();

            CreateMap<CompanySlide, CompanySlideCreateModel>(MemberList.None).ReverseMap();

            CreateMap<CompanySlide, CompanySlideEditModel>(MemberList.None).ReverseMap();

            CreateMap<CompanySlide, CompanySlideListModel>(MemberList.None).ReverseMap();

            CreateMap<CompanySlide, CompanySlideBulkEditModel>(MemberList.None).ReverseMap();
        }
    }
}