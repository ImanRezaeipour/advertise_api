using System;

namespace Advertise.Core.Objects
{
    public class Select2Object
    {
        public string children { get; set; }
        public bool disabled { get; set; }
        public Guid id { get; set; }
        public int level { get; set; }
        public string text { get; set; }
        public int count { get; set; }
        public Guid related_id { get; set; }
    }
}