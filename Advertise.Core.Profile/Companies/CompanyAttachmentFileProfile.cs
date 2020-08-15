using Advertise.Core.Domain.Companies;
using Advertise.Core.Model.Companies;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Companies
{
    public class CompanyAttachmentFileProfile : BaseProfile
    {
        public CompanyAttachmentFileProfile()
        {
            CreateMap<CompanyAttachmentFile, CompanyAttachmentFileModel>(MemberList.None).ReverseMap();
        }
    }
}