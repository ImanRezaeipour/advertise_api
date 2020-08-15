using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Advertise.Core.Managers.File;
using NReco.VideoConverter;
using NReco.VideoInfo;

namespace Advertise.Core.Managers.Video
{
    public class VideoManager : IVideoManager
    {
        #region Public Methods

        public async Task ConvertVideoAsync(string filePath)
        {
            var ffMpegConverter = new FFMpegConverter();
            var convertSettings = new ConvertSettings
            {
                AudioCodec = "copy", // or audio codec for re-encoding
                VideoCodec = "copy", // or video codec for re-encoding
                CustomOutputArgs = " -map 0:v:0 -map 1:a:0 "
            };
            var input = new[]
            {
                new FFMpegInput($"{Path.GetFileName(filePath)}")
            };
            var output = $"{Path.GetFileNameWithoutExtension(filePath)}_Converted.mp4";
            ffMpegConverter.ConvertMedia(input, output, null, convertSettings);
        }

        public async Task CutDownAsync(string sourceFile, int? startTime, int? endTime)
        {
            if (startTime == null)
                startTime = 0;
            if (endTime == null)
                endTime = new FFProbe().GetMediaInfo(sourceFile).Streams.Length;
            if (startTime >= endTime)
                return;

            var destinationFile = Path.GetFileNameWithoutExtension(sourceFile) + "_cut" + Path.GetExtension(sourceFile);
            var ffMpegConverter = new FFMpegConverter();
            var convertSettings = new ConvertSettings
            {
                Seek = startTime,
                MaxDuration = endTime - startTime,
                VideoCodec = "copy",
                AudioCodec = "copy"
            };
            ffMpegConverter.ConvertMedia(sourceFile, null, destinationFile, null, convertSettings);
        }

        public System.Drawing.Image DrawText(string text, Font font, Color textColor, Color backColor)
        {
            var img = new Bitmap(1, 1);
            var drawing = Graphics.FromImage(img);
            var textSize = drawing.MeasureString(text, font);
            img.Dispose();
            drawing.Dispose();
            img = new Bitmap((int)textSize.Width, (int)textSize.Height);
            drawing = Graphics.FromImage(img);
            drawing.Clear(backColor);
            Brush textBrush = new SolidBrush(textColor);
            drawing.DrawString(text, font, textBrush, 0, 0);
            drawing.Save();
            textBrush.Dispose();
            drawing.Dispose();
            return img;
        }

        public async Task GenerateThumbnailAsync(string filePath)
        {
            var ffMpegConverter = new FFMpegConverter();
            var outputFile = Path.Combine(HttpContext.Current.Server.MapPath(FileConst.VideosWebPath), Path.GetFileNameWithoutExtension(filePath) + "_thumb.jpg");

            ffMpegConverter.GetVideoThumbnail(filePath,outputFile);
        }

        public async Task<MediaInfo> ResolveMetadataAsync(string filePath)
        {
            var ffProbe = new FFProbe();
            var videoInfo = ffProbe.GetMediaInfo(filePath);

            return videoInfo;
        }

        public async Task WatermarkMediaAsync(string filePath, string companyName, VideoWatermarkType watermarkType, VideoWatermarkPositionType watermarkPosition, string companyLogoFilePath)
        {
            var watermarkPath = Path.Combine(HttpContext.Current.Server.MapPath(@"~/Files/Users/Video/Web/Watermark"), Path.GetFileNameWithoutExtension(filePath) + "_Watermarked.mp4");
            const string outPutPath = @"~/Files/Users/Video/Web";
            var text = $"{companyName.ToLower()}.novinak.com";
            var fullName = $@"{outPutPath}\Image\{text}.png".Replace(" / ", "-");
            var image = DrawText($"{text}", new Font("Alberta", 8, FontStyle.Regular), Color.FromArgb(40, 255, 255, 255), Color.Transparent);//Color.White
            image.Save(fullName, ImageFormat.Png);

            var outPutVideo = watermarkPath;
            var wrap = new FFMpegConverter();
            wrap.Invoke($" -i {filePath} -i {image}  -filter_complex  \"overlay=main_w-overlay_w-5:main_h-overlay_h-5\" - codec:a copy {outPutVideo}");
        }

        public async Task WatermarkWithCustomTextAsync(string filePath)
        {
            var videoResolution = new FFProbe().GetMediaInfo(filePath).Streams.Select(p => p.Width).FirstOrDefault();
            var fontSize = videoResolution / 30;
            var padding = videoResolution / 48;

            var inputFile = filePath;
            var outputFile = Path.Combine(HttpContext.Current.Server.MapPath(FileConst.VideosWebPath), Path.GetFileNameWithoutExtension(filePath) + "_watermarked.mp4");
            var text = $"Alias.novinak.com";
            var ffMpeg = new FFMpegConverter();
            var imagePath = HttpContext.Current.Server.MapPath(FileConst.WatermarkIcon);
            ffMpeg.Invoke($" -i {filePath} -i {imagePath}  -filter_complex  \"overlay=10:10\" -codec:a copy {outputFile}");

           // ffMpeg.Invoke($" -i {filePath} -vf \"drawtext = fontfile = alb.ttf: fontsize = {fontSize} : fontcolor = white@0.25: text = '{text}' : borderw = 0.5 :x = w - text_w - {padding} :y = h - text_h - {padding}\"  -acodec copy {outputFile}");
        }

        #endregion Public Methods
    }
}