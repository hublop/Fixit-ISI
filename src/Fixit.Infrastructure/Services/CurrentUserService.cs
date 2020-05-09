using System.Linq;
using System.Security.Claims;
using Fixit.Application.Common.Services;
using Microsoft.AspNetCore.Http;

namespace Fixit.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var stringUserId = httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            UserId = string.IsNullOrEmpty(stringUserId) ? (int?) null : int.Parse(stringUserId);

            IsAuthenticated = UserId != null;
        }

        public int? UserId { get; }

        public bool IsAuthenticated { get; }
        public bool IsUser(int userid)
        {
            return UserId == userid;
        }


    }
}