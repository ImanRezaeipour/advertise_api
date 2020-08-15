using Advertise.Core.Domain.Companies;
using Advertise.Core.Model.Companies;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Companies
{
    public class CompanyOfficialProfile : BaseProfile
    {
        public CompanyOfficialProfile()
        {
            CreateMap<CompanyOfficial, CompanyOfficialModel>(MemberList.None)
                .ForMember(dest => dest.CompanyTitle, opt => opt.MapFrom(src => src.Company.Title))
                .ForMember(dest => dest.CompanyAlias, opt => opt.MapFrom(src => src.Company.Alias));

            CreateMap<CompanyOfficialModel, CompanyOfficial>(MemberList.None);

            CreateMap<CompanyOfficial, CompanyOfficialCreateModel>(MemberList.None).ReverseMap();
            
            CreateMap<CompanyOfficial, CompanyOfficialListModel>(MemberList.None).ReverseMap();

            CreateMap<CompanyOfficial, CompanyOfficialEditModel>(MemberList.None).ReverseMap();
        }
    }
}