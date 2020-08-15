using System;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Common
{
    public class RequestModel
    {
        public Guid Id { get; set; }
        public AnnounceType AnnounceType { get; set; } 
        public AnnouncePositionType AnnouncePositionType { get; set; }
        public DurationType DurationType { get; set; }
        public int Count { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public bool IsAll { get; set; }
    }
}