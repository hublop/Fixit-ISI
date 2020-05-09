using System.Threading.Tasks;

namespace Fixit.Application.Common.Services.Identity
{
    public interface IJwtProvider
    {
        Task<JsonWebToken> GenerateAccessAndRefreshTokenAsync(string email, string password);
        Task<JsonWebToken> GenerateAccessAndRefreshTokenAsync(int userId, string refreshToken);
    }
}