using System.Threading.Tasks;

namespace Advertise.Core.Managers.Image
{
    public interface IImageBuilder
    {
        Task<bool> ProductImageProcessAsync(string filePath);
        Task<bool> CompanyImagesFileProcessAsync(string filePath);
        Task<bool> CompanyCoverFileProcessAsync(string filePath);
        Task<bool> LogoProcessAsync(string filePath);
        Task<bool> ProductImagsProcessAsync(string filePath, string path);
        Task<bool> CreateThumbFileAsync(string filePath);
        Task<bool> TranparentWatermarkAsync(string filePath);
        Task<bool> TransparentWatermarkWithCustomTextAsync(string filePath,string text);
    }
}