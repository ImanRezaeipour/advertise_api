using System.Threading.Tasks;

namespace Advertise.Service.Companies
{
    public interface ICompanyReserveService
    {
        Task<bool> IsExistByAlias(string alias);
    }
}