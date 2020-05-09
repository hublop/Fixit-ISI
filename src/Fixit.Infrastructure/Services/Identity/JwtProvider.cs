using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Fixit.Application.Common.Services.Identity;
using Fixit.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Fixit.Infrastructure.Services.Identity
{
    internal class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _jwtOptions;
        private readonly IRefreshTokenProvider _refreshTokenProvider;
        private readonly UserManager<User> _userManager;

        public JwtProvider(
            IOptions<JwtOptions> jwtOptions,
            IRefreshTokenProvider refreshTokenProvider,
            UserManager<User> userManager)
        {
            _jwtOptions = jwtOptions.Value;
            _refreshTokenProvider = refreshTokenProvider;
            _userManager = userManager;
            ThrowIfInvalidOptions(_jwtOptions);
        }

        private async Task<JsonWebToken> GenerateAccessAndRefreshTokenAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(),
                    ClaimValueTypes.Integer64),
            };

            var roles = await _userManager.GetRolesAsync(user);

            if (roles != null)
            {
                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            }

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var refreshToken = _refreshTokenProvider.GenerateRefreshToken(user.Id, user.Email);
            await _refreshTokenProvider.SaveRefreshTokenAsync(user.Id, user.Email, refreshToken);

            var jsonWebToken = new JsonWebToken
            {
                AccessToken = encodedJwt,
                RefreshToken = refreshToken,
                ExpiresIn = _jwtOptions.Expiration,
                User = new JsonWebTokenUser
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Roles = roles?.ToList(),
                    Id = user.Id
                }
            };

            return jsonWebToken;
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() -
                                  new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);

        private static void ThrowIfInvalidOptions(JwtOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtOptions.JtiGenerator));
            }
        }

        public async Task<JsonWebToken> GenerateAccessAndRefreshTokenAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return null;
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, password);

            if (!passwordValid)
            {
                return null;
            }

            return await GenerateAccessAndRefreshTokenAsync(user);
        }

        public async Task<JsonWebToken> GenerateAccessAndRefreshTokenAsync(int userId, string refreshToken)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return null;
            }

            if (user.RefreshToken != refreshToken)
            {
                return null;
            }

            return await GenerateAccessAndRefreshTokenAsync(user);
        }
    }
}