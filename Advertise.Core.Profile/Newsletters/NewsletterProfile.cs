using Advertise.Core.Domain.Newsletters;
using Advertise.Core.Model.Newsletters;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Newsletters
{
    public class NewsletterProfile : BaseProfile
    {
        public NewsletterProfile()
        {
            CreateMap<NewsletterCreateModel, Newsletter>(MemberList.None).ReverseMap();

            CreateMap<Newsletter, NewsletterModel>(MemberList.None).ReverseMap();

            CreateMap<Newsletter, NewsletterListModel>(MemberList.None).ReverseMap();
        }
    }
}