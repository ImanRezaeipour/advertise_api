using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Companies
{
    public class CompanyImageDetailModel : BaseModel
    {
        public string FileDimension { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public Guid Id { get; set; }
        public int Order { get; set; }
        public string RejectDescription { get; set; }
        public StateType State { get; set; }
        public string Title { get; set; }
    }
}