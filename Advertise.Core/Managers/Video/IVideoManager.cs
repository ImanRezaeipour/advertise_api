using System.Drawing;
using System.Threading.Tasks;
using NReco.VideoInfo;

namespace Advertise.Core.Managers.Video
{
    public interface IVideoManager
    {
        Task CutDownAsync(string sourceFile, int? startTime, int? endTime);
        Task WatermarkMediaAsync(string filePath, string companyName, VideoWatermarkType watermarkType, VideoWatermarkPositionType watermakPosition, string companyLogoFilePath);
        Task GenerateThumbnailAsync(string filePath);
        Task<MediaInfo> ResolveMetadataAsync(string filePath);
        Task WatermarkWithCustomTextAsync(string filePath);
        Task ConvertVideoAsync(string filePath);
        System.Drawing.Image DrawText(string text, Font font, Color textColor, Color backColor);
    }
}