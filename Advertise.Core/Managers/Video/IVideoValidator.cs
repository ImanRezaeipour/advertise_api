using System.Threading.Tasks;

namespace Advertise.Core.Managers.Video
{
    public interface IVideoValidator
    {
        Task GetFormatAsync(string file);
    }
}
