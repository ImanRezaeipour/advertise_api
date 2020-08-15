using Advertise.Core.Domain.Companies;
using Advertise.Core.Model.Companies;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Companies
{
    public class CompanyImageFileProfile : BaseProfile
    {
        public CompanyImageFileProfile()
        {
            CreateMap<CompanyImageFile, CompanyImageFileModel>(MemberList.None).ReverseMap();

            CreateMap<CompanyImage, CompanyImageFileModel>(MemberList.None).ReverseMap();
        }
    }
}