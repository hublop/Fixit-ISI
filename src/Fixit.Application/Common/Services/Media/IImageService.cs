using System.Threading.Tasks;

namespace Fixit.Application.Common.Services.Media
{
    public interface IImageService
    {
        Task<ImageUploadResult> UploadAsync(ImageUploadParameters parameters);
    }
}