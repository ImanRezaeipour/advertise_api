using Advertise.Core.Domain.Companies;
using Advertise.Core.Model.Companies;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Companies
{
    public class CompanyRateProfile : BaseProfile
    {
        public CompanyRateProfile()
        {
            CreateMap<CompanyRate, CompanyRateModel>(MemberList.None).ReverseMap();
        }
    }
}