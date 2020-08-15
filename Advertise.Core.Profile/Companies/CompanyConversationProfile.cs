using Advertise.Core.Domain.Companies;
using Advertise.Core.Model.Companies;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Companies
{
    public class CompanyConversationProfile : BaseProfile
    {
        public CompanyConversationProfile()
        {
            CreateMap<CompanyConversation, CompanyConversationCreateModel>(MemberList.None).ReverseMap();

            CreateMap<CompanyConversation, CompanyConversationEditModel>(MemberList.None).ReverseMap();

            CreateMap<CompanyConversation, CompanyConversationListModel>(MemberList.None).ReverseMap();

            CreateMap<CompanyConversation, CompanyConversationMyListModel>(MemberList.None).ReverseMap();
            
            CreateMap<CompanyConversationListModel, CompanyConversationModel>(MemberList.None).ReverseMap();
            
            CreateMap<CompanyConversationModel, CompanyConversationListModel>(MemberList.None).ReverseMap();

            CreateMap<CompanyConversation, CompanyConversationModel>(MemberList.None).ReverseMap();
        }
    }
}
