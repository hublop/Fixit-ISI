using System.Threading.Tasks;
using Fixit.Application.Common.Services.Identity;
using Fixit.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Fixit.Infrastructure.Services.Identity
{
    public class PasswordManager : IPasswordManager
    {
        private readonly UserManager<User> _userManager;

        public PasswordManager(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> IsPasswordValid(int userId, string password)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                return false;
            }

            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<bool> UpdatePassword(int userId, string oldPassword, string newPassword)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                return false;
            }

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

            return result.Succeeded;
        }
    }
}