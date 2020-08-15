using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Extensions;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Companies;
using Advertise.Data.DbContexts;

namespace Advertise.Service.Companies
{
    public class CompanyAttachmentFilService : ICompanyAttachmentFilService
    {
        #region Private Fields

        private readonly IDbSet<CompanyAttachmentFile> _companyAttachmentFileRepository;

        #endregion Private Fields

        #region Public Constructors

        public CompanyAttachmentFilService( IUnitOfWork unitOfWork)
        {
            _companyAttachmentFileRepository = unitOfWork.Set<CompanyAttachmentFile>();
        }
        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(CompanyAttachmentFileSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).CountAsync();
        }

        public async Task<CompanyAttachmentFile> FindByIdAsync(Guid companyAttachmentFileId)
        {
            return _companyAttachmentFileRepository.FirstOrDefault(model => model.Id == companyAttachmentFileId);
        }

        public async Task<IList<CompanyAttachmentFile>> GetByRequestAsync(CompanyAttachmentFileSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public IQueryable<CompanyAttachmentFile> QueryByRequest(CompanyAttachmentFileSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companyAttachmentFiles = _companyAttachmentFileRepository.AsNoTracking().AsQueryable();
            if (model.CompanyAttachmentId.HasValue)
                companyAttachmentFiles = companyAttachmentFiles.Where(m => m.CompanyAttachmentId == model.CompanyAttachmentId);

            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;

            companyAttachmentFiles = companyAttachmentFiles.OrderBy($"{model.SortMember} {model.SortDirection}");

            return companyAttachmentFiles;
        }

        #endregion Public Methods
    }
}