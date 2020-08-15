using Advertise.Core.Domain.Tags;
using Advertise.Core.Model.Companies;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Companies
{
    public class CompanyTagProfile : BaseProfile
    {
        public CompanyTagProfile()
        {
            CreateMap<Tag, CompanyTagSearchModel>(MemberList.None).ReverseMap();
        }
    }
}