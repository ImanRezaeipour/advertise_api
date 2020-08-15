using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Guarantees;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Guarantees;
using Advertise.Core.Objects;

namespace Advertise.Service.Guarantees
{
    public interface IGuaranteeService
    {
        Task<int> CountByRequestAsync(GuaranteeSearchModel model);
        Task CreateByViewModelAsync(GuaranteeCreateModel model);
        Task DeleteByIdAsync(Guid id);
        Task EditByViewMoodelAsync(GuaranteeEditModel model);
        Task<Guarantee> FindByIdAsync(Guid id);
        Task<IList<SelectList>> GetAsSelectListAsync();
        Task<IList<Guarantee>> GetByRequestAsync(GuaranteeSearchModel model);
        IQueryable<Guarantee> QueryByRequest(GuaranteeSearchModel model);
        Task<IList<Select2Object>> GetAsSelect2ObjectAsync();
    }
}