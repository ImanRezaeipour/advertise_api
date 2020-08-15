using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Companies
{
    public class CompanyQuestionDetailModel : BaseModel
    {
        public string Body { get; set; }
        public Guid Id { get; set; }
        public string RejectDescription { get; set; }
        public StateType State { get; set; }
        public string Title { get; set; }
    }
}