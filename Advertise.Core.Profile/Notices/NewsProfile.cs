using Advertise.Core.Domain.Notices;
using Advertise.Core.Model.Notices;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Notices
{
    public class NoticeProfile : BaseProfile
    {
        public NoticeProfile()
        {
            CreateMap<Notice, NoticeCreateModel>(MemberList.None).ReverseMap();

            CreateMap<Notice, NoticeDetailModel>(MemberList.None).ReverseMap();

            CreateMap<Notice, NoticeListModel>(MemberList.None).ReverseMap();

            CreateMap<Notice, NoticeModel>(MemberList.None).ReverseMap();

            CreateMap<Notice, NoticeSearchModel>(MemberList.None).ReverseMap();

            CreateMap<NoticeEditModel, Notice>(MemberList.None).ReverseMap();
        }
    }
}