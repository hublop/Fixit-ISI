using System.Threading.Tasks;

namespace Fixit.Application.Common.Services.Identity
{
    public interface IRefreshTokenProvider
    {
        Task<bool> ValidateRefreshTokenAsync(int userId, string refreshToken);
        Task SaveRefreshTokenAsync(int userId, string userEmail, RefreshToken refreshToken);
        RefreshToken GenerateRefreshToken(int userId, string userEmail);
    }
}