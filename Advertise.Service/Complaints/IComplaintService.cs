using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Complaints;
using Advertise.Core.Model.Complaints;

namespace Advertise.Service.Complaints
{
    public interface IComplaintService {
        Task  CreateByViewModel(ComplaintCreateModel model);
        Task  DeleteByIdAsync(Guid complaintId);
        Task<Complaint> FindByIdAsync(Guid complaintId);
        Task<IList<Complaint>> GetByRequestAsync(ComplaintSearchModel model);
        Task<int> CountByRequestAsync(ComplaintSearchModel model);
       IQueryable<Complaint> QueryByRequest(ComplaintSearchModel model);
    }
}