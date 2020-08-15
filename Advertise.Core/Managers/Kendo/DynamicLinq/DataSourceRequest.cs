using System.Collections.Generic;

namespace Advertise.Core.Managers.Kendo.DynamicLinq
{
    public class DataSourceRequest
    {
        public int Take { get; set; }

        public int Skip { get; set; }

        public IEnumerable<Sort> Sort { get; set; }

        public Filter Filter { get; set; }
    }
}
