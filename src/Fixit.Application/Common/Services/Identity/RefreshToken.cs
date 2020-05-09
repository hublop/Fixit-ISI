using System;

namespace Fixit.Application.Common.Services.Identity
{
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime IssuedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }
    }
}