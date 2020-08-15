using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Companies;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Companies
{
    public class CompanyHourService : ICompanyHourService
    {
        #region Private Fields

        private readonly IDbSet<Company> _companyRepository;
        private readonly IDbSet<CompanyHour> _companyHourRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public CompanyHourService(IMapper mapper, IUnitOfWork unitOfWork, IWebContextManager webContextManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _webContextManager = webContextManager;
            _companyRepository = unitOfWork.Set<Company>();
            _companyHourRepository = unitOfWork.Set<CompanyHour>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(CompanyHourSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companyHours = await QueryByRequest(model).CountAsync();

            return companyHours;
        }

        public async Task CreateByViewModelAsync(CompanyHourCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var Hour = _mapper.Map<CompanyHour>(model);
            var company = await _companyRepository.FirstOrDefaultAsync(m => m.Id == _webContextManager.CurrentUserId);
            Hour.CompanyId = company.Id;
            Hour.CreatedById = _webContextManager.CurrentUserId;

            _companyHourRepository.Add(Hour);

            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid HourId)
        {
            var Hour = await _companyHourRepository.FirstOrDefaultAsync(model => model.Id == HourId);
            _companyHourRepository.Remove(Hour);

            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task EditByViewModelAsync(CompanyHourEditModel model)
        {
            //var companyId = _webContextManager.CurrentCompanyId;
            var companyHour1 = await _companyHourRepository.Where(m => m.CompanyId == model.CompanyId).ToListAsync();
            foreach (var item in companyHour1)
            {
                _companyHourRepository.Remove(item);
            }
            foreach (var item in model.CompanyHours)
            {
                if (item.DayOfWeek != null)
                {
                    var companyHour = _mapper.Map<CompanyHour>(item);
                //companyHour.CompanyId = _webContextManager.CurrentCompanyId;
                    companyHour.CreatedById = _webContextManager.CurrentUserId;
                _companyHourRepository.Add(companyHour);
                }
                
            }
            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task<CompanyHour> FindAsync(Guid companyHourId)
        {
            var companyHour = await _companyHourRepository
                .FirstOrDefaultAsync(model => model.Id == companyHourId);
    
            return companyHour;
        }
    
        public async Task<CompanyHour> FindByUserIdAsync(Guid userId)
        {
            var companyHour = await _companyHourRepository
                .FirstOrDefaultAsync(model => model.CreatedById == userId);
    
            return companyHour;
        }
    
        public IQueryable<CompanyHour> QueryByRequest(CompanyHourSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
    
            var companyHours = _companyHourRepository.AsNoTracking().AsQueryable()
                .Include(m => m.CreatedBy)
                .Include(m => m.CreatedBy.Meta)
                .Include(m => m.Company);
            if (model.CreatedById.HasValue)
                companyHours = companyHours.Where(m => m.CreatedById == model.CreatedById);
            if (model.CompanyId.HasValue)
                companyHours = companyHours.Where(m => m.CompanyId == model.CompanyId);
            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;
            companyHours = companyHours.OrderBy($"{model.SortMember} {model.SortDirection}");
    
            return companyHours;
        }
    
        public async Task<IList<CompanyHour>> GetByRequestAsync(CompanyHourSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
    
            var companyHours = await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
    
            return companyHours;
        }
    
        public async Task RemoveRangeAsync(IList<CompanyHour> companyHours)
        {
            if (companyHours == null)
                throw new ArgumentNullException(nameof(companyHours));
    
            foreach (var companyHour in companyHours)
                _companyHourRepository.Remove(companyHour);
        }
    
        public async Task SeedAsync()
        {
            throw new NotImplementedException();
        }
    
        #endregion Public Methods
    }
}