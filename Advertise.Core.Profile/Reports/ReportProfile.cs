using Advertise.Core.Domain.Reports;
using Advertise.Core.Model.Reports;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Reports
{
    public class ReportProfile : BaseProfile
    {
        public ReportProfile()
        {
            CreateMap<Report, ReportCreateModel>(MemberList.None).ReverseMap();

            CreateMap<Report, ReportEditModel>(MemberList.None).ReverseMap();

            CreateMap<Report, ReportModel>(MemberList.None);
            
            CreateMap< ReportModel, Report>(MemberList.None);
        }
    }
}