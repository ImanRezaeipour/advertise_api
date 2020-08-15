using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Companies
{
    public class CompanyVideoFileModel : BaseModel
    {
        public string FileExtension { get; set; }
        public string FileName { get; set; }
        public string FileResolution { get; set; }
        public string FileSize { get; set; }
        public Guid Id { get; set; }
        public bool? IsWatermark { get; set; }
        public int? Order { get; set; }
        public string RejectDescription { get; set; }
        public StateType? State { get; set; }
        public string ThumbName { get; set; }
        public string WatermarkName { get; set; }
    }
}