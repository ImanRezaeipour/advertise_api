using Advertise.Core.Domain.Companies;
using Advertise.Core.Model.Companies;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Companies
{
    public class CompanyVideoFileProfile: BaseProfile
    {
        public CompanyVideoFileProfile()
        {
            CreateMap<CompanyVideoFile, CompanyVideoFileModel>(MemberList.None).ReverseMap();
        }
    }
}
