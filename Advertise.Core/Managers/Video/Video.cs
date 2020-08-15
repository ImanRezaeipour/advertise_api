namespace Advertise.Core.Managers.Video
{
    public class Video
    {
        public string Format { get; internal set; }

        public string ColorModel { get; internal set; }

        public string FrameSize { get; internal set; }

        public int? BitRateKbs { get; internal set; }

        public double Fps { get; internal set; }
    }
}