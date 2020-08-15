using Advertise.Core.Domain.Statistics;
using Advertise.Core.Model.Statistics;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Statistics
{
    public class StatisticProfile : BaseProfile
    {
        public StatisticProfile()
        {
            CreateMap<Statistic, StatisticCreateModel>(MemberList.None).ReverseMap();
        }
    }
}