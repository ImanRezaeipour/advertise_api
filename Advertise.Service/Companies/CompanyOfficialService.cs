using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Advertise.Core.Common;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.File;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Companies;
using Advertise.Core.Objects;
using Advertise.Data.DbContexts;
using AutoMapper;
using MvcSiteMapProvider.Linq;

namespace Advertise.Service.Companies
{
    public class CompanyOfficialService : ICompanyOfficialService
    {
        #region Private Fields

        private readonly IDbSet<CompanyOfficial> _companyOfficialRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public CompanyOfficialService(IMapper mapper, IUnitOfWork unitOfWork, IWebContextManager contextManager)
        {
            _mapper = mapper;
            _webContextManager = contextManager;
            _companyOfficialRepository = unitOfWork.Set<CompanyOfficial>();
            _unitOfWork = unitOfWork;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task ApproveByViewModelAsync(CompanyOfficialEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companyOfficial = await FindByIdAsync(model.Id);
            _mapper.Map(model, companyOfficial);
            companyOfficial.IsApprove = true;
            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task<int> CountByRequestAsync(CompanyOfficialSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var list = await GetByRequestAsync(model);
            return list.Count;
        }

        public async Task CreateByViewModelAsync(CompanyOfficialCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companyOfficial = _mapper.Map<CompanyOfficial>(model);
            //companyOfficial.CompanyId = _webContextManager.CurrentCompanyId;
                _companyOfficialRepository.Add(companyOfficial);
            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task<IList<FineUploaderObject>> GetFileBusinessLicenseAsFineUploaderModelByIdAsync(Guid companyOfficialId)
        {
            return (await _companyOfficialRepository.AsNoTracking()
                    .Where(model => model.Id == companyOfficialId)
                    .Select(model => new
                    {
                        model.Id,
                        model.BusinessLicenseFileName
                    }).ToListAsync())
                .Select(model => new FineUploaderObject
                {
                    id = model.Id.ToString(),
                    uuid = model.Id.ToString(),
                    name = model.BusinessLicenseFileName,
                    thumbnailUrl = FileConst.ImagesWebPath.PlusWord(model.BusinessLicenseFileName),
                    size = new FileInfo(HttpContext.Current.Server.MapPath(FileConst.ImagesWebPath.PlusWord(model.BusinessLicenseFileName))).Length.ToString()
                }).ToList();
        }

        public async Task<IList<FineUploaderObject>> GetFileNationalCardAsFineUploaderModelByIdAsync(Guid companyOfficialId)
        {
            return (await _companyOfficialRepository.AsNoTracking()
                    .Where(model => model.Id == companyOfficialId)
                    .Select(model => new
                    {
                        model.Id,
                        model.NationalCardFileName
                    }).ToListAsync())
                .Select(model => new FineUploaderObject
                {
                    id = model.Id.ToString(),
                    uuid = model.Id.ToString(),
                    name = model.NationalCardFileName,
                    thumbnailUrl = FileConst.ImagesWebPath.PlusWord(model.NationalCardFileName),
                    size = new FileInfo(HttpContext.Current.Server.MapPath(FileConst.ImagesWebPath.PlusWord(model.NationalCardFileName))).Length.ToString()
                }).ToList();
        }

        public async Task<IList<FineUploaderObject>> GetFileOfficialNewspaperAddressAsFineUploaderModelByIdAsync(Guid companyOfficialId)
        {
            return (await _companyOfficialRepository.AsNoTracking()
                    .Where(model => model.Id == companyOfficialId)
                    .Select(model => new
                    {
                        model.Id,
                        model.OfficialNewspaperAddress
                    }).ToListAsync())
                .Select(model => new FineUploaderObject
                {
                    id = model.Id.ToString(),
                    uuid = model.Id.ToString(),
                    name = model.OfficialNewspaperAddress,
                    thumbnailUrl = FileConst.ImagesWebPath.PlusWord(model.OfficialNewspaperAddress),
                    size = new FileInfo(HttpContext.Current.Server.MapPath(FileConst.ImagesWebPath.PlusWord(model.OfficialNewspaperAddress))).Length.ToString()
                }).ToList();
        }

        public async Task EditByViewModelAsync(CompanyOfficialEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companyOfficial = await FindByIdAsync(model.Id);
            companyOfficial.IsApprove = null;
            _mapper.Map(model, companyOfficial);
            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task<CompanyOfficial> FindByIdAsync(Guid companyOfficialId)
        {
            return await _companyOfficialRepository.SingleOrDefaultAsync(model => model.Id == companyOfficialId);
        }

        public async Task<IList<CompanyOfficial>> GetByRequestAsync(CompanyOfficialSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequestAsync(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public IQueryable<CompanyOfficial> QueryByRequestAsync(CompanyOfficialSearchModel model)
        {
            var query = _companyOfficialRepository.AsNoTracking().AsQueryable();

            if (model.Term.HasValue())
                query = query.Where(m => m.Company.Title.Contains(model.Term));

            if (model.CompanyId.HasValue)
                query = query.Where(m => m.CompanyId == model.CompanyId);

            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;

            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;

            query = query.OrderBy($"{model.SortMember} {model.SortDirection}");
            return query;
        }
        
        public async Task RejectByViewModelAsync(CompanyOfficialEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companyOfficial = await FindByIdAsync(model.Id);
            _mapper.Map(model, companyOfficial);
            companyOfficial.IsApprove = false;
            await _unitOfWork.SaveAllChangesAsync();
        }

        #endregion Public Methods
    }
}