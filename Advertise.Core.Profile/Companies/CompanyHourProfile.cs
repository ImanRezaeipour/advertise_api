using Advertise.Core.Domain.Companies;
using Advertise.Core.Model.Companies;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Companies
{
    public class CompanyHourProfile : BaseProfile
    {
        public CompanyHourProfile()
        {
            CreateMap<CompanyHour, CompanyHourModel>(MemberList.None).ReverseMap();

            CreateMap<CompanyHour, CompanyHourEditModel>(MemberList.None);

            CreateMap<CompanyHourEditModel, CompanyHour>(MemberList.None)
                .ForMember(dest => dest.DayOfWeek ,opt => opt.Ignore());
        }
    }
}