using System;

namespace Fixit.Application.Common.Services.Media
{
    public class ImageUploadResult
    {
        public string PublicId { get; set; }
        public Uri Uri { get; set; }
        public bool IsSuccessful { get; set; }
    }
}