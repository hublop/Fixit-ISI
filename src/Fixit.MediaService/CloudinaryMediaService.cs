using System;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace Fixit.MediaService
{
    public class CloudinaryMediaService : IImageService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryMediaService(IOptions<CloudinaryMediaServiceOptions> optionsAccessor)
        {
            var options = optionsAccessor?.Value ?? throw new ArgumentNullException(nameof(optionsAccessor));

            var account = new Account(options.CloudName, options.ApiKey, options.ApiSecret);
            _cloudinary = new Cloudinary(account);
        }

        public async Task<ImageUploadResult> UploadAsync(ImageUploadParameters parameters)
        {
            try
            {
                var cloudinaryUploadParams = new ImageUploadParams
                {
                    File = new FileDescription($@"{parameters.ImageBas64Uri}")
                };

                var cloudinaryUploadResult = await _cloudinary.UploadAsync(cloudinaryUploadParams);

                return new ImageUploadResult
                {
                    IsSuccessful = true,
                    PublicId = cloudinaryUploadResult.PublicId,
                    Uri = cloudinaryUploadResult.SecureUri
                };
            }
            catch (Exception e)
            {
                // log
                Console.WriteLine(e);

                return new ImageUploadResult
                {
                    IsSuccessful = false
                };
            }
        }
    }
}