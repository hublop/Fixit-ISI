using System;

namespace Fixit.Application.Common.Services.Identity
{
    public class JsonWebToken
    {
        public string AccessToken { get; set; }
        public JsonWebTokenUser User { get; set; }
        public DateTime ExpiresIn { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}