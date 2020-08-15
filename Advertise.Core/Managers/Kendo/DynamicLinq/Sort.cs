using System.Runtime.Serialization;

namespace Advertise.Core.Managers.Kendo.DynamicLinq
{
    [DataContract]
    public class Sort
    {
        [DataMember(Name = "field")]
        public string Field { get; set; }

        [DataMember(Name = "dir")]
        public string Dir { get; set; }

        public string ToExpression()
        {
            return Field + " " + Dir;
        }
    }
}
