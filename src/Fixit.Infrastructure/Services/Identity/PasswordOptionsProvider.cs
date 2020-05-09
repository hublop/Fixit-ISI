using Fixit.Application.Common.Services.Identity;
using Fixit.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Fixit.Infrastructure.Services.Identity
{
    internal class PasswordOptionsProvider : IPasswordOptionsProvider
    {
        private readonly UserManager<User> _userManager;

        public PasswordOptionsProvider(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public PasswordOptions GetPasswordOptions()
        {
            return _userManager.Options.Password;
        }
    }
}