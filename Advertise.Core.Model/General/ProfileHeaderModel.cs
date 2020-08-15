using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.General
{
    public class ProfileHeaderModel : BaseModel
    {
        public string AvatarFileName { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyTitle { get; set; }
        public Guid CreatedById { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Followers { get; set; }
        public string FullName { get; set; }
        public Guid Id { get; set; }
        public string UserCode { get; set; }
    }
}