using Advertise.Core.Domain.Companies;
using Advertise.Core.Model.Companies;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Companies
{
    public class CompanySocialProfile : BaseProfile
    {
        public CompanySocialProfile()
        {
            CreateMap<CompanySocial, CompanySocialCreateModel>(MemberList.None).ReverseMap();

            CreateMap< CompanySocialEditModel, CompanySocial>(MemberList.None)
                .ForMember(dest => dest.CompanyId, opt => opt.Ignore());

            CreateMap<CompanySocial, CompanySocialEditModel>(MemberList.None);
              
            CreateMap<CompanySocial, CompanySocialListModel>(MemberList.None).ReverseMap();

            CreateMap<CompanySocial, CompanySocialSearchModel>(MemberList.None).ReverseMap();

            CreateMap<CompanySocial, CompanySocialModel>(MemberList.None)
                .ForMember(dest => dest.CompanyTitle, opts => opts.MapFrom(src => src.Company.Title));
        }
    }
}