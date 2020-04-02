using System.Threading.Tasks;

namespace Fixit.MediaService
{
    public interface IImageService
    {
        Task<ImageUploadResult> UploadAsync(ImageUploadParameters parameters);
    }
}