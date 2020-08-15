using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using System.Web;
using Advertise.Core.Common;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Managers.File;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Companies;
using Advertise.Data.DbContexts;
using AutoMapper;
using FineUploaderObject = Advertise.Core.Objects.FineUploaderObject;

namespace Advertise.Service.Companies
{
    public class CompanyBalanceService : ICompanyBalanceService
    {
        #region Private Fields

        private readonly IDbSet<CompanyBalance> _companyBalanceRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public CompanyBalanceService(IUnitOfWork unitOfWork, IMapper mapper, IEventPublisher eventPublisher, IWebContextManager webContextManager)
        {
            _unitOfWork = unitOfWork;
            _companyBalanceRepository = unitOfWork.Set<CompanyBalance>();
            _mapper = mapper;
            _eventPublisher = eventPublisher;
            _webContextManager = webContextManager;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(CompanyBalanceSearchModel model)
        {
            return await QueryByRequest(model).CountAsync();
        }

        public async Task CreateByViewModelAsync(CompanyBalanceCreateModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            var companyBalance = _mapper.Map<CompanyBalance>(viewModel);
            companyBalance.CreatedById = _webContextManager.CurrentUserId;
            _companyBalanceRepository.Add(companyBalance);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityInserted(companyBalance);
        }

        public async Task DeleteByIdAsync(Guid companyBalanceId)
        {
            var companyBalance = await _companyBalanceRepository.FirstOrDefaultAsync(model => model.Id == companyBalanceId);
            _companyBalanceRepository.Remove(companyBalance);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(companyBalance);
        }

        public async Task EditByViewModelAsync(CompanyBalanceEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companyBalance = await _companyBalanceRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            _mapper.Map(model, companyBalance);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(companyBalance);
        }

        public async Task<CompanyBalance> FindByIdAsync(Guid companyBalanceId)
        {
            return await _companyBalanceRepository.FirstOrDefaultAsync(model => model.Id == companyBalanceId);
        }

        public async Task<IList<CompanyBalance>> GetByRequestAsync(CompanyBalanceSearchModel model)
        {
            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public async Task<IList<FineUploaderObject>> GetFileAsFineUploaderModelByIdAsync(Guid companyBalanceId)
        {
            return (await _companyBalanceRepository.AsNoTracking()
                    .Where(model => model.Id == companyBalanceId)
                    .Select(model => new
                    {
                        model.Id,
                        model.AttachmentFile
                    }).ToListAsync())
                .Select(model => new FineUploaderObject
                {
                    id = model.Id.ToString(),
                    uuid = model.Id.ToString(),
                    name = model.AttachmentFile,
                    thumbnailUrl = FileConst.ImagesWebPath.PlusWord(model.AttachmentFile),
                    size = new FileInfo(HttpContext.Current.Server.MapPath(FileConst.ImagesWebPath.PlusWord(model.AttachmentFile))).Length.ToString()
                }).ToList();
        }

        public IQueryable<CompanyBalance> QueryByRequest(CompanyBalanceSearchModel model)
        {
            var companyBalances = _companyBalanceRepository.AsNoTracking().AsQueryable();

            if (model.CreatedOn != null)
                companyBalances = companyBalances.Where(m => m.CreatedOn == model.CreatedOn);
            if (model.CompanyId.HasValue)
                companyBalances = companyBalances.Where(m => m.CompanyId == model.CompanyId);
            if (model.Term.HasValue())
                companyBalances = companyBalances.Where(m => m.Depositor .Contains(model.Term)|| m.Description.Contains(model.Term) || m.DocumentNumber.Contains(model.Term) || m.IssueTracking.Contains(model.Term) );
            if (model.CreatedById.HasValue)
                companyBalances = companyBalances.Where(m => m.CreatedById == model.CreatedById);
            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Asc;

            companyBalances = companyBalances.OrderBy($"{model.SortMember} {model.SortDirection}");

            return companyBalances;
        }

        #endregion Public Methods
    }
}