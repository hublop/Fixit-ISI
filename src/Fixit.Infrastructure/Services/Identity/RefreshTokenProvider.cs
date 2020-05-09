using System;
using System.Threading.Tasks;
using Fixit.Application.Common.Services.Identity;
using Fixit.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Fixit.Infrastructure.Services.Identity
{
    public class RefreshTokenProvider : IRefreshTokenProvider
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtOptions _jwtOptions;

        public RefreshTokenProvider(IOptions<JwtOptions> optionsAccessor, UserManager<User> userManager)
        {
            _userManager = userManager;
            _jwtOptions = optionsAccessor.Value ?? throw new ArgumentNullException(nameof(optionsAccessor.Value));
        }

        public async Task<bool> ValidateRefreshTokenAsync(int userId, string refreshToken)
        {
            var originUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (originUser == null)
            {
                return false;
            }

            return originUser.RefreshToken == refreshToken && originUser.RefreshTokenExpiryDate > DateTime.UtcNow;
        }

        public async Task SaveRefreshTokenAsync(int userId, string userEmail, RefreshToken refreshToken)
        {
            var originUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == userEmail && x.Id == userId);

            originUser.RefreshToken = refreshToken.Token;
            originUser.RefreshTokenExpiryDate = refreshToken.ExpiresUtc;
            originUser.RefreshTokenCreatedDate = refreshToken.IssuedUtc;

            await _userManager.UpdateAsync(originUser);
        }

        public RefreshToken GenerateRefreshToken(int userId, string userEmail)
        {
            return new RefreshToken
            {
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(_jwtOptions.RefreshTokenExpiryTimeInMinutes),
                Token = Guid.NewGuid().ToString().Replace("-", "")
            };
        }
    }
}