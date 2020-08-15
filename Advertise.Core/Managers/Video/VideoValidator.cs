using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Advertise.Core.Exceptions;
using NReco.VideoInfo;

namespace Advertise.Core.Managers.Video
{
    public class VideoValidator : IVideoValidator
    {
        #region Public Methods

        public async Task GetFormatAsync(string file)
        {
            string[] format =
            {
                ".wmv", ".mov", ".qt", ".ts", ".3gp", ".3gpp", ".3g2", ".3gp2", ".mpg", ".mpeg", ".mp1",
                ".mp2", ".m1v", ".m1a", ".m2a", ".mpa", ".mpv", ".mpv2", ".mpe", ".mp4", ".m4a", ".m4p", ".m4b", ".m4r",
                ".m4v",
                ".avi", ".flv", ".f4v", ".f4p", ".f4a", ".f4b", ".vob", ".lsf", ".lsx", ".asf", ".asr", ".asx", ".webm",
                ".mkv"
            };
            var pathExtension = Path.GetExtension(file);
            if (format.All(fileEx => fileEx != pathExtension))
                throw new ValidationException("نوع فایل مجاز نیست");
            //return format.Any(fileEx => fileEx == pathExtension) ? null : "نوع فایل مجاز نیست";
            file = HttpContext.Current.Server.MapPath(file);
            // تعیین کیفیت مجاز برای فایل های ویدیویی ارسالی
            var ffProbe = new FFProbe();
            var x = ffProbe.GetMediaInfo(file);
            var width = 0;
            foreach (var stream in x.Streams)
            {
                if (stream.CodecType == "video")
                {
                    width = stream.Width;
                }
            }
            if (width < 360 && width > 1280)
                throw new ValidationException("کیفیت نمایش خارج از محدوده مجاز است");

            //تعیین حجم مجاز فایل های ویدیویی
            var size = new FileInfo(file).Length;
           
            if (size <= 5)
                throw new ValidationException("اندازه فایل قابل قبول نیست");
        }

        #endregion Public Methods
    }
}