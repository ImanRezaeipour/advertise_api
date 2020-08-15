using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Users;

namespace Advertise.Core.Domain.Reports
{
    public class Report : BaseEntity
    {
        [Column(TypeName = "xml")]
        public virtual string Content { get; set; }
        public virtual string Description { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual string Name { get; set; }
        public virtual int Order { get; set; }
        public virtual string Title { get; set; }
        [NotMapped]
        public virtual XElement XmlContent
        {
            get { return XElement.Parse(Content); }
            set { Content = value.ToString(); }
        }
        public virtual bool? HasChild { get; set; }
        public virtual int? Level { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
        public virtual Report Parent { get; set; }
        public virtual Guid? ParentId { get; set; }
    }
}