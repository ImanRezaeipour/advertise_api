using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Manufacturers
{
    public class ManufacturerModel : BaseModel
    {
        public Guid Id { get; set; }
        public CountryType? Country { get; set; }
        public string Name { get; set; }
    }
}