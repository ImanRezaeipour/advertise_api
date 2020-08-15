using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Advertise.Service.Users
{
    public interface IUserPasswordValidator
    {
        bool RequireDigit { get; set; }
        int RequiredLength { get; set; }
        bool RequireLowercase { get; set; }
        bool RequireNonLetterOrDigit { get; set; }
        bool RequireUppercase { get; set; }
        bool IsDigit(char c);
        bool IsLetterOrDigit(char c);
        bool IsLower(char c);
        bool IsUpper(char c);
        Task<IdentityResult> ValidateAsync(string item);
    }
}