using System;

namespace Advertise.Core.Objects
{
    public class JsTreeObject
    {
        public Guid? Id { get; set; }
        public bool IsSelect { get; set; }
        public Guid? ParentId { get; set; }
        public string Title { get; set; }
    }
}