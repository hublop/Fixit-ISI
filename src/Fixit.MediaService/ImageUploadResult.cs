using System;

namespace Fixit.MediaService
{
    public class ImageUploadResult
    {
        public string PublicId { get; set; }
        public Uri Uri { get; set; }
        public bool IsSuccessful { get; set; }
    }
}