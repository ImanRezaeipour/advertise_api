using Advertise.Core.Domain.Companies;
using Advertise.Core.Model.Companies;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Companies
{
    public class CompanyBalanceProfile : BaseProfile
    {
        public CompanyBalanceProfile()
        {
            CreateMap<CompanyBalance, CompanyBalanceViewModel>(MemberList.None).ReverseMap();

            CreateMap<CompanyBalance, CompanyBalanceCreateModel>(MemberList.None).ReverseMap();
            
            CreateMap<CompanyBalance, CompanyBalanceListModel>(MemberList.None).ReverseMap();
            
            CreateMap<CompanyBalance, CompanyBalanceEditModel>(MemberList.None).ReverseMap();
        }
    }
}