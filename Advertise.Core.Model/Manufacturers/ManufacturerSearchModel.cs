using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Manufacturers
{
    public class ManufacturerSearchModel:BaseSearchModel
    {
        public CountryType? Country { get; set; }
        public Guid? CreatedById { get; set; }
    }
}